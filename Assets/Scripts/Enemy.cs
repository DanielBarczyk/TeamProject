using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int hp=2;
    public int defense=0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hp<=0)
        {
            Debug.Log("hp<=0, destroying\n");
            Destroy(this.gameObject);
        }
    }

    public void ShootAt(int damage)
    {
        int actual_damage=damage-defense;
        if(actual_damage>0){
            hp-=actual_damage;
            Debug.Log("took "+actual_damage+" damage\n");
        }
    }
}
