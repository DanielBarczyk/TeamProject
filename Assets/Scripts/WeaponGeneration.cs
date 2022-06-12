using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGeneration : MonoBehaviour
{
    [SerializeField] GameObject standardGunPrefab;
    [SerializeField] GameObject shotgunPrefab;
    [SerializeField] GameObject sniperPrefab;


    public GameObject generateWeapon(Vector3 position, Quaternion rotation)
    {
        float ran=UnityEngine.Random.value;
        if(ran < 0.5f)
            return generateStandardGun(position,rotation);
        if(ran > 0.5f && ran < 0.75f)
            return generateShotgun(position,rotation);
        return generateSniper(position,rotation);
    }

    GameObject generateStandardGun(Vector3 position, Quaternion rotation)
    {
        GameObject generatedWeapon = Instantiate(standardGunPrefab, position, rotation);
        generatedWeapon.GetComponent<WeaponBehaviour>().weapon = generatedWeapon.GetComponent<Weapon>();
        generatedWeapon.GetComponent<Weapon>().name = "Standard Gun";
        generatedWeapon.GetComponent<Weapon>().range = 20.0f;

        float ran = UnityEngine.Random.value * 4 + 1;
        generatedWeapon.GetComponent<Weapon>().atk = Mathf.FloorToInt(ran);
        
        ran = UnityEngine.Random.value * 40 + 80;
        generatedWeapon.GetComponent<Weapon>().delay = Mathf.FloorToInt(ran);
        
        return generatedWeapon;
    }

    GameObject generateShotgun(Vector3 position, Quaternion rotation)
    {
        GameObject generatedWeapon = Instantiate(shotgunPrefab, position, rotation);
        generatedWeapon.GetComponent<WeaponBehaviour>().weapon = generatedWeapon.GetComponent<Weapon>();
        
        generatedWeapon.GetComponent<Weapon>().name = "Shotgun";
        generatedWeapon.GetComponent<Weapon>().range = 20.0f;
        
        float ran = UnityEngine.Random.value * 10 + 5;
        generatedWeapon.GetComponent<Weapon>().atk = Mathf.FloorToInt(ran);
        
        ran = UnityEngine.Random.value * 120 + 300;
        generatedWeapon.GetComponent<Weapon>().delay = Mathf.FloorToInt(ran);
        
        return generatedWeapon;
    }


    GameObject generateSniper(Vector3 position, Quaternion rotation)
    {
        GameObject generatedWeapon = Instantiate(sniperPrefab, position, rotation);
        generatedWeapon.GetComponent<WeaponBehaviour>().weapon = generatedWeapon.GetComponent<Weapon>();
        
        generatedWeapon.GetComponent<Weapon>().name = "Sniper Rifle";
        generatedWeapon.GetComponent<Weapon>().range = 120.0f;
        
        float ran = UnityEngine.Random.value * 5 + 3;
        generatedWeapon.GetComponent<Weapon>().atk = Mathf.FloorToInt(ran);
        
        ran = UnityEngine.Random.value * 60 + 150;
        generatedWeapon.GetComponent<Weapon>().delay = Mathf.FloorToInt(ran);
        
        return generatedWeapon;
    }

}
