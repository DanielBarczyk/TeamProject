using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collect : MonoBehaviour
{
    
    void onTriggerEnter(Collider other){
        this.gameObject.SetActive(false);
    }
}
