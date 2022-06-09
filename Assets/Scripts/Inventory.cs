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
    
    public Weapon equippedWeapon;
    void Start(){
        visible=false;
        inventoryScreen.SetActive(false);
        equippedWeapon=new Weapon();
        weaponCount=0;
        AddWeapon(new Weapon());
        AddWeapon(new Weapon("Strong and Slow",10,420,4.0f));
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

    public bool AddWeapon(Weapon weapon){
        if(weaponCount<8)
        {
            weaponCount++;
            GameObject button = (GameObject)Instantiate(buttonPrefab); 
            button.GetComponentInChildren<Text>().text=weapon.name+"\nAttack: "+weapon.atk+"\ndelay: "+weapon.delay;
            button.transform.SetParent(weaponPanel.transform,false);
            button.GetComponent<Button>().onClick.AddListener(
                () => {ChangeWeapons(button,weapon);}
            );
            return true;
        }
        return false;
    }
    void UpdateWeapons()
    {
        currentWeapon.GetComponent<Text>().text="Current Weapon:\n"+equippedWeapon.name+"\nAttack: "+equippedWeapon.atk+"\ndelay: "+equippedWeapon.delay;
    }
    void ChangeWeapons(GameObject button, Weapon weapon)
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            Destroy(button);
            GameObject weapondrop = (GameObject)Instantiate(weaponPrefab,this.gameObject.GetComponent<Transform>().position+transform.forward*2,this.gameObject.GetComponent<Transform>().rotation);
            weapondrop.name=name;
            weapondrop.GetComponent<WeaponBehaviour>().weapon=weapon;
        }
        else
        {
            equippedWeapon=weapon;
            UpdateWeapons();
        }
    }
    public bool AddItem(string name)
    {
        if(itemCount<24)
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
