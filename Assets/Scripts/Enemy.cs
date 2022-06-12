using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int hp=2;
    public int defense=0;
    public int atk=20;
    int moveframes;
    float currentx=0;
    float currentz=0;
    public int movetype=0;
    public float movementspeed;

    // Start is called before the first frame update
    void Start()
    {
        moveframes=0;
        if(movetype==0)
            movementspeed=0;
        else
            movementspeed=0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        if(hp<=0)
        {
            Debug.Log("hp<=0, destroying\n");
            Destroy(this.gameObject);
        }
        if(movetype==1)
            this.GetComponent<Collider>().transform.Translate(0,0,0.01f);
        if(movetype==2)
            HorizontalMovement();
    }

    void HorizontalMovement()
    {
        if(moveframes==0)
        {
            
            float ran=UnityEngine.Random.value;
            if(ran<0.25f)
            {
                print("going left");
                currentx=movementspeed;
                currentz=0;
            }
            if(ran>0.25f&&ran<0.5f)
            {
                print("going right");
                currentx=(-1)*movementspeed;
                currentz=0;
            }
            if(ran>0.5f&&ran<0.75f)
            {
                print("going forward");
                currentz=movementspeed;
                currentx=0;
            }
            if(ran>0.75)
            {
                print("going down");
                currentz=(-1)*movementspeed;
                currentx=0;
            }
            moveframes=30;
        }
        this.GetComponent<Collider>().transform.Translate(currentx,0,currentz);
        moveframes--;
    }
    
    public void ShootAt(int damage)
    {
        int actual_damage=damage-defense;
        if(actual_damage>0){
            hp-=actual_damage;
            Debug.Log("took "+actual_damage+" damage\n");
        }
    }

    void OnCollisionEnter(Collision collision)
    { 
        if(collision.gameObject.tag=="Player")
        {
            if(collision.gameObject.GetComponent<Collisions>().currentIframes==0)
            {
                collision.gameObject.GetComponent<PlayerHealth>().takeDamage(atk);
                collision.gameObject.GetComponent<Collisions>().currentIframes=30;
            }
        }
    }

    void OnCollisionHold(Collision collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            if(collision.gameObject.GetComponent<Collisions>().currentIframes==0)
            {
                collision.gameObject.GetComponent<PlayerHealth>().takeDamage(atk);
                collision.gameObject.GetComponent<Collisions>().currentIframes=30;
            }
        }
    }
}
