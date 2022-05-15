using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string name;
    // Start is called before the first frame update
    void Start()
    {
        name=this.gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUp(Inventory inventory)
    {
        if(inventory.AddItem(name))
        Destroy(this.gameObject);
    }
}
