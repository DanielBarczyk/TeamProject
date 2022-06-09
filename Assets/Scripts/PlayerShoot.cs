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
        if(!this.gameObject.GetComponentInParent<Inventory>().visible)
        {
            Ray ray=this.gameObject.GetComponent<MouseLook>().ray;

            bool isEnemyInRange;
            RaycastHit hit;
            isEnemyInRange=Physics.Raycast(ray.origin,ray.direction,out hit,this.gameObject.GetComponentInParent<Inventory>().equippedWeapon.range);
            if(Input.GetMouseButtonDown(0))
            {
                if(currentDelay==0)
                {
                    if(isEnemyInRange)
                    {
                        Collider looking_at=hit.collider;
                        if(looking_at.tag=="Enemy")
                        {
                            looking_at.GetComponent<Enemy>().ShootAt(this.gameObject.GetComponentInParent<Inventory>().equippedWeapon.atk);
                            isEnemyInRange=false;
                        }
                    }
                    currentDelay=this.gameObject.GetComponentInParent<Inventory>().equippedWeapon.delay;
                }
                else
                {
                    Debug.Log("Cooling down\n");
                }
            }
            if(currentDelay>0){
                currentDelay--;
            }

            bool isSomethingInRange=Physics.Raycast(ray.origin,ray.direction,out hit,this.gameObject.GetComponent<MouseLook>().rayDistance);
            
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
                if(looking_at.tag=="Weapon")
                {
                    prompts.SetActive(true);
                    if(Input.GetKeyDown("e"))
                    {
                        looking_at.GetComponent<WeaponBehaviour>().PickUp(this.gameObject.GetComponentInParent<Inventory>());
                        prompts.SetActive(false);
                    }
                }
                if(looking_at.tag!="Weapon"&&looking_at.tag!="Item")
                    prompts.SetActive(false);
            }
            else
                prompts.SetActive(false);
        }
    }
}
