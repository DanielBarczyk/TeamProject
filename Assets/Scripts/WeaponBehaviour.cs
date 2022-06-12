using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    public Weapon weapon;

    public void PickUp(Inventory inventory)
    {
        if(inventory.AddWeapon(weapon))
        Destroy(this.gameObject);
    }
}
