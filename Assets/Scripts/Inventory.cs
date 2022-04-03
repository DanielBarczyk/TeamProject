using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    bool visible;
    int cubecount=0;

    void Update()
    {
        if(Input.GetKey("i")){
            visible=!visible;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collected");
        cubecount++;
    }
}
