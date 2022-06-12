using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessTerrain : MonoBehaviour
{
    const float scale = 5f;
    const float moveThresholdBeforeUpdate = 25;
    const float squareMoveThresholdBeforeUpdate = moveThresholdBeforeUpdate * moveThresholdBeforeUpdate;

    public LODInfo[] detailLevels;
    public static float maxViewDistance;

    public Transform viewer;
    public static Vector2 viewerPosition;

    Vector2 viewerPositionOld;
    static MapGenerator mapGenerator;
    static EnemyGeneration enemyGeneration;
    static WeaponGeneration weaponGeneration;
    int chunkSize;
    int chunksVisibleInViewDistance;
    public Material mapMaterial;
    public GameObject waterObjectPrefab;
    public GameObject treeObjectPrefab;
    
    Dictionary<Vector2, TerrainChunk> terrainChunkDictionary = new Dictionary<Vector2, TerrainChunk>();
    static List<TerrainChunk> terrainChunksVisibleLastUpdate = new List<TerrainChunk>();

    void Start() {
        chunkSize = MapGenerator.mapChunkSize - 1;
        maxViewDistance = detailLevels[detailLevels.Length - 1].visibleDistanceThreshold;
        chunksVisibleInViewDistance = Mathf.RoundToInt(maxViewDistance / chunkSize);
        mapGenerator = FindObjectOfType<MapGenerator>();
        enemyGeneration = FindObjectOfType<EnemyGeneration>();
        weaponGeneration = FindObjectOfType<WeaponGeneration>();
        UpdateVisibleChunks();
    }

    // Update is called once per frame
    void Update() {
        viewerPosition = new Vector2(viewer.position.x, viewer.position.z) / scale;
        if((viewerPositionOld - viewerPosition).sqrMagnitude > squareMoveThresholdBeforeUpdate) {
            viewerPositionOld = viewerPosition;
            UpdateVisibleChunks();
        }
    }

    void UpdateVisibleChunks() {
        for (int i = 0; i < terrainChunksVisibleLastUpdate.Count; i++) {
            terrainChunksVisibleLastUpdate[i].SetVisible(false);
        }
        terrainChunksVisibleLastUpdate.Clear();

        int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / chunkSize);
        int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / chunkSize);

        for (int yOffset = -chunksVisibleInViewDistance; yOffset <= chunksVisibleInViewDistance; yOffset++) {
            for (int xOffset = -chunksVisibleInViewDistance; xOffset <= chunksVisibleInViewDistance; xOffset++) {
                Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);
                
                if (terrainChunkDictionary.ContainsKey(viewedChunkCoord)) {
                    terrainChunkDictionary[viewedChunkCoord].UpdateTerrainChunk();
                } else {
                    terrainChunkDictionary.Add(viewedChunkCoord, new TerrainChunk(viewedChunkCoord, chunkSize, detailLevels, transform, mapMaterial, waterObjectPrefab, treeObjectPrefab));
                }
            }
        }
    }

    public class TerrainChunk {
        GameObject meshObject;
        GameObject waterObjectInstance;
        GameObject treeObjectBase;
        List<GameObject> treeObjects;
        List<GameObject> enemyObjects;
        List<GameObject> weaponObjects;
        Vector2 position;
        Bounds bounds;
        int size;

        MeshRenderer meshRenderer;
        MeshFilter meshFilter;
        MeshCollider meshCollider;

        LODInfo[] detailLevels;
        LODMesh[] lodMeshes;

        MapData mapData;
        bool mapDataReceived;
        int previousLODIndex = -1;

        public TerrainChunk(Vector2 coord, int size, LODInfo[] detailLevels, Transform parent, Material material, GameObject waterObject, GameObject treeObject) {
            this.detailLevels = detailLevels;
            this.size = size;
            position = coord * size;
            Vector3 positionV3 = new Vector3(position.x, 0, position.y);
            Vector3 waterLevel = new Vector3(0, 0.5f, 0);
            bounds = new Bounds(position, Vector2.one * size);

            treeObjects = new List<GameObject>();
            enemyObjects = new List<GameObject>();
            weaponObjects = new List<GameObject>();
            treeObjectBase = treeObject;

            meshObject = new GameObject("Terrain Chunk");
            meshRenderer = meshObject.AddComponent<MeshRenderer>();
            meshFilter = meshObject.AddComponent<MeshFilter>();
            meshCollider = meshObject.AddComponent<MeshCollider>();

            meshRenderer.material = material;
            meshObject.transform.position = positionV3 * scale;
            meshObject.transform.parent = parent;
            meshObject.transform.localScale = Vector3.one * scale;

            waterObjectInstance = Instantiate(waterObject, (positionV3 + waterLevel) * scale, Quaternion.identity);
            waterObjectInstance.transform.parent = meshObject.transform;

            SetVisible(false);

            lodMeshes = new LODMesh[detailLevels.Length];
            for (int i = 0; i < detailLevels.Length; i++) {
                lodMeshes[i] = new LODMesh(detailLevels[i].lod, UpdateTerrainChunk);
            }

            mapGenerator.RequestMapData(position, OnMapDataReceived);
        }

        void OnMapDataReceived(MapData mapData) {
            this.mapData = mapData;
            mapDataReceived = true;

            Texture2D texture = TextureGenerator.TextureFromColourMap(mapData.colourMap, MapGenerator.mapChunkSize, MapGenerator.mapChunkSize);
            meshRenderer.material.mainTexture = texture;

            populateChunk(treeObjects, treeObjectBase);
            generateEnemies();
            generatedWeapons();

            UpdateTerrainChunk();
        }

        void populateChunk(List<GameObject> objectList, GameObject prefab) {
            Quaternion quaternion = Quaternion.Euler(270, 0, 0);

            for (int i = 0; i < 1000; i++) {
                int randomX = (int) Mathf.Floor(Random.Range(0, MapGenerator.mapChunkSize));
                int randomY = (int) Mathf.Floor(Random.Range(0, MapGenerator.mapChunkSize));
                float height = mapData.heightMap[randomX, randomY];
                
                float topLeftX = (MapGenerator.mapChunkSize - 1) / -2f;
		        float topLeftZ = (MapGenerator.mapChunkSize - 1) / 2f;

                Vector3 randomOffset = new Vector3(topLeftX + randomX, 0, topLeftZ - randomY);

                if (height > 0.5 && height < 0.8) {
                    randomOffset.y = mapGenerator.meshHeightCurve.Evaluate(height) * mapGenerator.meshHeightMultiplier;
                    randomOffset.x += position.x;
                    randomOffset.z += position.y;
                    GameObject tree = Instantiate(treeObjectBase, randomOffset * scale, quaternion);
                    tree.transform.parent = meshObject.transform;
                    tree.transform.localScale = Vector3.one * scale / 3;
                    treeObjects.Add(tree);
                } 
            }
        }

        void generateEnemies() {
            for (int i = 0; i < 100; i++) {
                int randomX = (int) Mathf.Floor(Random.Range(0, MapGenerator.mapChunkSize));
                int randomY = (int) Mathf.Floor(Random.Range(0, MapGenerator.mapChunkSize));
                float height = mapData.heightMap[randomX, randomY];
                
                float topLeftX = (MapGenerator.mapChunkSize - 1) / -2f;
		        float topLeftZ = (MapGenerator.mapChunkSize - 1) / 2f;

                Vector3 randomOffset = new Vector3(topLeftX + randomX, 0, topLeftZ - randomY);
                
                randomOffset.y = mapGenerator.meshHeightCurve.Evaluate(height) * mapGenerator.meshHeightMultiplier + 1;
                randomOffset.x += position.x;
                randomOffset.z += position.y;
                GameObject enemy = enemyGeneration.generateEnemy(randomOffset * scale, Quaternion.identity);
                enemy.transform.parent = meshObject.transform;
                enemy.transform.localScale = Vector3.one * scale / 4;
                enemyObjects.Add(enemy);
            }
        }

        void generatedWeapons() {
            for (int i = 0; i < 40; i++) {
                int randomX = (int) Mathf.Floor(Random.Range(0, MapGenerator.mapChunkSize));
                int randomY = (int) Mathf.Floor(Random.Range(0, MapGenerator.mapChunkSize));
                float height = mapData.heightMap[randomX, randomY];
                
                float topLeftX = (MapGenerator.mapChunkSize - 1) / -2f;
		        float topLeftZ = (MapGenerator.mapChunkSize - 1) / 2f;

                Vector3 randomOffset = new Vector3(topLeftX + randomX, 0, topLeftZ - randomY);

                randomOffset.y = mapGenerator.meshHeightCurve.Evaluate(height) * mapGenerator.meshHeightMultiplier + 1;
                randomOffset.x += position.x;
                randomOffset.z += position.y;
                GameObject weapon = weaponGeneration.generateWeapon(randomOffset * scale, Quaternion.identity);
                weapon.transform.parent = meshObject.transform;
                weapon.transform.localScale = Vector3.one * scale / 5;
                weaponObjects.Add(weapon);
            }
        }

        public void UpdateTerrainChunk() {
            if (!mapDataReceived) {
                return;
            }

            float viewerDistanceFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));
            bool visible = viewerDistanceFromNearestEdge <= maxViewDistance;

            if (visible) {
                int lodIndex = 0;

                for (int i = 0; i < detailLevels.Length - 1; i++) {
                    if (viewerDistanceFromNearestEdge > detailLevels[i].visibleDistanceThreshold) {
                        lodIndex = i + 1;
                    } else {
                        break;
                    }
                }

                if (lodIndex != previousLODIndex) {
                    LODMesh lodMesh = lodMeshes[lodIndex];
                    if (lodMesh.hasMesh) {
                        previousLODIndex = lodIndex;
                        meshFilter.mesh = lodMesh.mesh;
                        meshCollider.sharedMesh = meshFilter.mesh;
                    } else if (!lodMesh.hasRequestedMesh) {
                        lodMesh.RequstMesh(mapData);
                    }
                }

                terrainChunksVisibleLastUpdate.Add(this);
            }

            SetVisible(visible);
        }

        public void SetVisible(bool visible) {
            meshObject.SetActive(visible);
        }

        public bool IsVisible() {
            return meshObject.activeSelf;
        }
    }

    class LODMesh {
        public Mesh mesh;
        public bool hasRequestedMesh;
        public bool hasMesh;
        int lod;
        System.Action updateCallback;

        public LODMesh(int lod, System.Action updateCallback) {
            this.lod = lod;
            this.updateCallback = updateCallback;
        }

        void OnMeshDataReceived(MeshData meshData) {
            mesh = meshData.CreateMesh();
            hasMesh = true;
            updateCallback();
        }

        public void RequstMesh(MapData mapData) {
            hasRequestedMesh = true;
            mapGenerator.RequestMeshData(mapData, lod, OnMeshDataReceived);
        }
    }
    [System.Serializable]
    public struct LODInfo {
        public int lod;
        public float visibleDistanceThreshold;
    }
}