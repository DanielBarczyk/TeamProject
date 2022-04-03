using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collect : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other){
        Debug.Log("The Cube has been obliterated\n");
        Destroy(this.gameObject);
        
    }
}
