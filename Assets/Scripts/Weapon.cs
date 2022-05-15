using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
  
    public string name;  
    public int atk;
    public int delay;
    // Start is called before the first frame update
    
    public Weapon()
    {
        name="Starting Weapon";
        atk=1;
        delay=120;
    }
    public Weapon(string name,int atk, int delay)
    {
        this.name=name;
        this.atk=atk;
        this.delay=delay;
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUp(Inventory inventory)
    {
        if(inventory.AddWeapon(name,atk,delay))
        Destroy(this.gameObject);
    }
}
