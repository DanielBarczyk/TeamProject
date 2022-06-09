using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
  
    public Weapon weapon;
    // Start is called before the first frame update
    
    public WeaponBehaviour(Weapon w)
    {
        weapon=w;
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
        if(inventory.AddWeapon(weapon))
        Destroy(this.gameObject);
    }
}
