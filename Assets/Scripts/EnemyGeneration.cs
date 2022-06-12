using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneration : MonoBehaviour
{


    [SerializeField] GameObject stationaryEnemyPrefab;
    [SerializeField] GameObject movingEnemyPrefab;

    public void generateEnemy(Vector3 position, Quaternion rotation)
    {
        float ran=UnityEngine.Random.value;
        if(ran<0.5f)
            generateStationaryEnemy(position,rotation);
        else
            generateMovingEnemy(position,rotation);
    }

    void generateStationaryEnemy(Vector3 position, Quaternion rotation)
    {
        GameObject generatedEnemy=(GameObject)Instantiate(stationaryEnemyPrefab,position,rotation);
        randomizeEnemyStats(generatedEnemy,1);
        generatedEnemy.GetComponent<Enemy>().movementspeed=0;
    }

    void generateMovingEnemy(Vector3 position, Quaternion rotation)
    {
        GameObject generatedEnemy=(GameObject)Instantiate(movingEnemyPrefab,position,rotation);
        randomizeEnemyStats(generatedEnemy,1);
    }

    void randomizeEnemyStats(GameObject generatedEnemy, int modifier)
    {
        float ran=UnityEngine.Random.value;
        ran*=10;
        generatedEnemy.GetComponent<Enemy>().hp=Mathf.FloorToInt(ran*modifier);
        ran=UnityEngine.Random.value;
        ran*=3;
        generatedEnemy.GetComponent<Enemy>().defense=Mathf.FloorToInt(ran*modifier);
        ran=UnityEngine.Random.value;
        ran*=40;
        generatedEnemy.GetComponent<Enemy>().atk=Mathf.FloorToInt(ran);
        ran=UnityEngine.Random.value;
        ran*=0.5f;
        generatedEnemy.GetComponent<Enemy>().movementspeed=Mathf.FloorToInt(ran);
    }
}
