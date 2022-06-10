using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    // Start is called before the first frame update
    public int currentIframes;
    void Start()
    {
        currentIframes=0;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentIframes>0)
            currentIframes--;
    }

  

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag=="Enemy"&&currentIframes==0)
        {
            print("I'm hit!");
            this.gameObject.GetComponentInParent<PlayerHealth>().takeDamage(hit.gameObject.GetComponent<Enemy>().atk);
            currentIframes=30;
        }
    }

}
