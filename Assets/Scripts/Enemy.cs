using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int hp=2;
    public int defense=0;
    public int atk=20;
    float currentx=0;
    float currentz=0;
    public int movetype=0;
    public float movementspeed;

    // Update is called once per frame
    void Update()
    {
        if(hp<=0)
        {
            EndlessTerrain.weaponGeneration.generateWeapon(this.transform.position, Quaternion.identity);
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
        float ran=UnityEngine.Random.value;
        if(ran<0.25f)
        {
            currentx=movementspeed;
            currentz=0;
        }
        if(ran>0.25f&&ran<0.5f)
        {
            currentx=(-1)*movementspeed;
            currentz=0;
        }
        if(ran>0.5f&&ran<0.75f)
        {
            currentz=movementspeed;
            currentx=0;
        }
        if(ran>0.75)
        {
            currentz=(-1)*movementspeed;
            currentx=0;
        }
        this.GetComponent<Collider>().transform.Translate(currentx * Time.deltaTime, 0, currentz * Time.deltaTime);
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
