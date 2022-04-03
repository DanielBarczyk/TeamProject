using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    bool visible;
    // Start is called before the first frame update
    void Start()
    {
        visible=false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("i")){
            visible=!visible;
        }
    }

    void onTriggerEnter(Collider other)
    {
        

    }
}
