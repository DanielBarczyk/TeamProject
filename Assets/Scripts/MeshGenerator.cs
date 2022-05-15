using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator {
    public static MeshData GenerateTerrainMesh(float[,] heightMap, float heightMultiplier, AnimationCurve globalHeightCurve, int levelOfDetail) {
        AnimationCurve heightCurve = new AnimationCurve(globalHeightCurve.keys);
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        float topLeftX = (width - 1) / -2f;
        float topLeftZ = (height - 1) / 2f;

        int meshSimplificationIncrement = (levelOfDetail == 0) ? 1 : levelOfDetail * 2;
        int verticesPerLine = (width - 1) / meshSimplificationIncrement + 1;

        MeshData meshData = new MeshData(verticesPerLine, verticesPerLine);
        int vertexIndex = 0;

        for (int y = 0; y < height; y += meshSimplificationIncrement) {
            for (int x = 0; x < width; x += meshSimplificationIncrement) {
                meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightCurve.Evaluate(heightMap[x, y]) * heightMultiplier, topLeftZ - y);
                meshData.uvs[vertexIndex] = new Vector2(x / (float) width, y / (float) height);
                if (x < width - 1 && y < height - 1) {
                    meshData.AddTriangle(vertexIndex, vertexIndex + verticesPerLine + 1, vertexIndex + verticesPerLine);
                    meshData.AddTriangle(vertexIndex + verticesPerLine + 1, vertexIndex, vertexIndex + 1);
                }

                vertexIndex++;
            }
        }

        return meshData;
    }
}

public class MeshData {
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;
    int triangleIndex = 0;
    public MeshData(int width, int height) {
        vertices = new Vector3[width * height];
        triangles = new int[(width - 1) * (height - 1) * 6];
        uvs = new Vector2[width * height];
    }

    public void AddTriangle(int a, int b, int c) {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        triangleIndex += 3;
    }

    public Mesh CreateMesh() {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }
}

/* [RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour {
    Mesh mesh;
    Vector3[] vertices;
    int[] triangles;

    public int xSize = 100;
    public int zSize = 100;

    // Start is called before the first frame update
    void Start() {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        CreateShape();
        UpdateMesh();
    }

    // Update is called once per frame
    void Update() {
        
    }

    void CreateShape() {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int i = 0; i <= zSize; i++) {
            for (int j = 0; j <= xSize; j++) {
                float y = Mathf.PerlinNoise(j * 0.3f, i * 0.3f) * 2f;
               vertices[i * (xSize + 1) + j] = new Vector3(j, y, i); 
            }
        }

        triangles = new int[xSize * zSize * 6];
        
        int vert = 0;
        int tris = 0;
        for (int j = 0; j < zSize; j++) {
            for (int i = 0; i < xSize; i++) {   
                triangles[tris] = vert;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;
                vert++;
                tris += 6;
            }
            vert++;
        }
    }

    void UpdateMesh() {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
} */
