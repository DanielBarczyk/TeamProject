using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public int playerAttack=1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray=this.gameObject.GetComponent<MouseLook>().ray;
        float rayDistance=this.gameObject.GetComponent<MouseLook>().rayDistance;

        RaycastHit hit;
        if(Physics.Raycast(ray.origin,ray.direction,out hit,rayDistance))
        {
            Collider looking_at=hit.collider;
            if(looking_at.tag=="Enemy")
            {
                //Debug.Log("Lookin' at an enemy\n");
                if(Input.GetMouseButtonDown(0))
                {
                    looking_at.GetComponent<Enemy>().ShootAt(playerAttack);
                }
            }

        }
        
    }
}
