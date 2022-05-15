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

    public GameObject cubeCount;
    public GameObject inventoryScreen;

    public GameObject currentWeapon;

    public GameObject allWeapons;

    bool visible;
    int cubecount;

    List<Weapon> weapons;
    public Weapon equippedWeapon;

    void Start(){
        visible=false;
        inventoryScreen.SetActive(false);
        cubeCount.GetComponent<Text>().text="Cubes Collected:0";
        cubecount=0;
        equippedWeapon=new Weapon();
        weapons=new List<Weapon>();
        weapons.Add(equippedWeapon);
        AddWeapon("Strong and Slow",10,420);
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

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collected");
        cubecount++;
        cubeCount.GetComponent<Text>().text="Cubes Collected:"+cubecount.ToString();
    }

    void AddWeapon(string name, int atk, int delay){
        weapons.Add(new Weapon(name,atk,delay));
        UpdateWeapons();
    }
    void UpdateWeapons()
    {
        currentWeapon.GetComponent<Text>().text="Current Weapon:\n"+equippedWeapon.name+"\nAttack: "+equippedWeapon.atk+"\ndelay: "+equippedWeapon.shotDelay;
        allWeapons.GetComponent<Text>().text="All Weapons:\n";
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
}
