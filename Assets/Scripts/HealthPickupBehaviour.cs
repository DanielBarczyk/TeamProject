using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupBehaviour : MonoBehaviour
{
    public int hp;

    public void Collect(Inventory inventory)
    {
        inventory.Heal(hp);
        Destroy(this.gameObject);
    }
}
