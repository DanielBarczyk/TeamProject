using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    public GameObject cubeCount;
    public GameObject inventoryScreen;
    bool visible;
    int cubecount;


    void Start(){
        visible=false;
        inventoryScreen.SetActive(false);
        cubeCount.GetComponent<Text>().text="Cubes Collected:0";
        cubecount=0;
    }
    void Update()
    {
        if(Input.GetKeyDown("i")){
            visible=!visible;
            inventoryScreen.SetActive(visible);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collected");
        cubecount++;
        cubeCount.GetComponent<Text>().text="Cubes Collected:"+cubecount.ToString();
    }
}
