using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{

    public class Weapon
    {
        public string name;
        public int atk;
        public int shotDelay;
        public Weapon()
        {
            name="Starting Weapon";
            atk=1;
            shotDelay=120;
        }
        public Weapon(string name,int atk, int shotDelay)
        {
            this.name=name;
            this.atk=atk;
            this.shotDelay=shotDelay;
        }
    }

    public class Item
    {
        public string name;
        public Item()
        {
            name="New Item";
        }
        public Item(string name)
        {
            this.name=name;
        }
    }

    public GameObject inventoryScreen;

    public GameObject currentWeapon;

    public GameObject allWeapons;

    public GameObject inventoryItems;

    bool visible;
    int cubecount;

    List<Weapon> weapons;
    List<Item> items;
    public Weapon equippedWeapon;

    void Start(){
        visible=false;
        inventoryScreen.SetActive(false);
        equippedWeapon=new Weapon();
        weapons=new List<Weapon>();
        weapons.Add(equippedWeapon);
        AddWeapon("Strong and Slow",10,420);
        items=new List<Item>();
        UpdateItems();
    }
    void Update()
    {
        if(Input.GetKeyDown("i")){
            visible=!visible;
            inventoryScreen.SetActive(visible);
        }
        if(inventoryScreen.activeSelf==true)
        {
            for(int i=1;i<10;i++)
            {
                if(Input.GetKeyDown(i.ToString()))
                {
                    ChangeWeapons(i);
                }
            }
        }
    }
    void AddWeapon(string name, int atk, int delay){
        if(weapons.Count<9)
            weapons.Add(new Weapon(name,atk,delay));
        UpdateWeapons();
    }
    void UpdateWeapons()
    {
        currentWeapon.GetComponent<Text>().text="Current Weapon:\n"+equippedWeapon.name+"\nAttack: "+equippedWeapon.atk+"\ndelay: "+equippedWeapon.shotDelay;
        allWeapons.GetComponent<Text>().text="All Weapons: "+weapons.Count+"/9\n";
        foreach(var val in weapons)
        {
            allWeapons.GetComponent<Text>().text+=val.name+"\nAttack: "+val.atk+"\ndelay: "+val.shotDelay+"\n\n";
        }
    }
    void ChangeWeapons(int arg)
    {
        if(arg<=weapons.Count&&arg>0)
        {
            equippedWeapon=weapons[arg-1];
            UpdateWeapons();
        }
    }

    public bool AddItem(string name)
    {
        if(items.Count<40)
        {
            items.Add(new Item(name));
            UpdateItems();
            return true;
        }
        return false;
    }

    void UpdateItems()
    {
        inventoryItems.GetComponent<Text>().text="Current Items:\n";
        foreach(var val in items)
        {
            inventoryItems.GetComponent<Text>().text+=val.name+"\n";
        }
    }
}
