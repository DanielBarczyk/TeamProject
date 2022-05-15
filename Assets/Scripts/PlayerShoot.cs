using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    int currentDelay;
    // Start is called before the first frame update
    void Start()
    {
        currentDelay=0;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray=this.gameObject.GetComponent<MouseLook>().ray;
        float rayDistance=this.gameObject.GetComponent<MouseLook>().rayDistance;

        RaycastHit hit;
        if(Input.GetMouseButtonDown(0))
        {
            if(currentDelay==0)
            {
                if(Physics.Raycast(ray.origin,ray.direction,out hit,rayDistance))
                {
                    Collider looking_at=hit.collider;
                    if(looking_at.tag=="Enemy")
                    {
                    //Debug.Log("Lookin' at an enemy\n");
                        looking_at.GetComponent<Enemy>().ShootAt(this.gameObject.GetComponentInParent<Inventory>().equippedWeapon.atk);
                    }
                }
                currentDelay=this.gameObject.GetComponentInParent<Inventory>().equippedWeapon.shotDelay;
            }
            else
            {
                Debug.Log("Cooling down\n");
            }
        }
        if(currentDelay>0){
            currentDelay--;
        }
    }
}
