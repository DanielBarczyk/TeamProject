using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    public GameObject inventoryScreen;

    public GameObject currentWeapon;

    public bool visible;

    int itemCount;
    int weaponCount;
    
    public string equippedName;
    public int equippedAtk;
    public int equippedDelay;
    void Start(){
        visible=false;
        inventoryScreen.SetActive(false);
        equippedName="Starting Weapon";
        equippedAtk=1;
        equippedDelay=120;
        weaponCount=0;
        AddWeapon("Starting Weapon",1,120);
        AddWeapon("Strong and Slow",10,420);
        UpdateWeapons();
        itemCount=0;
    }
    void Update()
    {
        if(Input.GetKeyDown("i")){
            visible=!visible;
            inventoryScreen.SetActive(visible);
        }
    }

    [SerializeField] Transform itemPanel;
    [SerializeField] Transform weaponPanel;
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] GameObject weaponPrefab;

    public bool AddWeapon(string name, int atk, int delay){
        if(weaponCount<8)
        {
            weaponCount++;
            GameObject button = (GameObject)Instantiate(buttonPrefab); 
            button.GetComponentInChildren<Text>().text=name+"\nAttack: "+atk+"\ndelay: "+delay;
            button.transform.SetParent(weaponPanel.transform,false);
            button.GetComponent<Button>().onClick.AddListener(
                () => {ChangeWeapons(button,name, atk, delay);}
            );
            return true;
        }
        return false;
    }
    void UpdateWeapons()
    {
        currentWeapon.GetComponent<Text>().text="Current Weapon:\n"+equippedName+"\nAttack: "+equippedAtk+"\ndelay: "+equippedDelay;
    }
    void ChangeWeapons(GameObject button, string name, int atk, int delay)
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Destroy(button);
            GameObject weapon = (GameObject)Instantiate(weaponPrefab,this.gameObject.GetComponent<Transform>().position+transform.forward*2,this.gameObject.GetComponent<Transform>().rotation);
            weapon.name=name;
            weapon.GetComponent<Weapon>().name=name;
            weapon.GetComponent<Weapon>().atk=atk;
            weapon.GetComponent<Weapon>().delay=delay;
        }
        else
        {
            equippedName=name;
            equippedAtk=atk;
            equippedDelay=delay;
            UpdateWeapons();
        }
    }
    public bool AddItem(string name)
    {
        if(itemCount<30)
        {
            itemCount++;
            GameObject button = (GameObject)Instantiate(buttonPrefab); 
            button.GetComponentInChildren<Text>().text=name;
            button.transform.SetParent(itemPanel.transform,false);
            button.GetComponent<Button>().onClick.AddListener(
                () => {removeButton(button,name);}
            );
            return true;
        }
        return false;
    }
    void removeButton(GameObject button,string name){
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Destroy(button);
            GameObject item = (GameObject)Instantiate(itemPrefab,this.gameObject.GetComponent<Transform>().position+transform.forward*2,this.gameObject.GetComponent<Transform>().rotation);
            item.name=name;
        }
    }

}
