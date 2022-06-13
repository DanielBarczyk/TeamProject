using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneration : MonoBehaviour
{


    [SerializeField] GameObject stationaryEnemyPrefab;
    [SerializeField] GameObject movingEnemyPrefab;

    public GameObject generateEnemy(Vector3 position, Quaternion rotation)
    {
        float ran = UnityEngine.Random.value;
        if(ran < 0.5f)
            return generateStationaryEnemy(position,rotation);
        return generateMovingEnemy(position,rotation);
    }

    GameObject generateStationaryEnemy(Vector3 position, Quaternion rotation)
    {
        GameObject generatedEnemy=(GameObject)Instantiate(stationaryEnemyPrefab,position,rotation);
        randomizeEnemyStats(generatedEnemy,1);
        generatedEnemy.GetComponent<Enemy>().movementspeed=0;
        return generatedEnemy;
    }

    GameObject generateMovingEnemy(Vector3 position, Quaternion rotation)
    {
        GameObject generatedEnemy=(GameObject)Instantiate(movingEnemyPrefab,position,rotation);
        randomizeEnemyStats(generatedEnemy,1);
        return generatedEnemy;
    }

    void randomizeEnemyStats(GameObject generatedEnemy, int modifier)
    {
        float ran = UnityEngine.Random.value * 10;
        generatedEnemy.GetComponent<Enemy>().hp = Mathf.FloorToInt(ran * modifier + 1);
        
        ran=UnityEngine.Random.value * 3;
        generatedEnemy.GetComponent<Enemy>().defense = Mathf.FloorToInt(ran*modifier);
        
        ran = UnityEngine.Random.value * 40;
        generatedEnemy.GetComponent<Enemy>().atk = Mathf.FloorToInt(ran);

        ran = UnityEngine.Random.value * 0.5f;
        generatedEnemy.GetComponent<Enemy>().movementspeed = Mathf.FloorToInt(ran);
    }
}
