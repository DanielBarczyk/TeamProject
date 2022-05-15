using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject prompts;
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

        bool isSomethingInRange;
        RaycastHit hit;
        isSomethingInRange=Physics.Raycast(ray.origin,ray.direction,out hit,rayDistance);
        if(Input.GetMouseButtonDown(0))
        {
            if(currentDelay==0)
            {
                if(isSomethingInRange)
                {
                    Collider looking_at=hit.collider;
                    if(looking_at.tag=="Enemy")
                    {
                        looking_at.GetComponent<Enemy>().ShootAt(this.gameObject.GetComponentInParent<Inventory>().equippedWeapon.atk);
                        isSomethingInRange=false;
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

        
        if(isSomethingInRange)
        {
            Collider looking_at=hit.collider;
            if(looking_at.tag=="Item")
            {
                prompts.SetActive(true);
                if(Input.GetKeyDown("e"))
                {
                    looking_at.GetComponent<Item>().PickUp(this.gameObject.GetComponentInParent<Inventory>());
                prompts.SetActive(false);
                }
            }
        }
            else
                prompts.SetActive(false);

    }
}
