using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGeneration : MonoBehaviour
{
    [SerializeField] GameObject standardGunPrefab;
    [SerializeField] GameObject shotgunPrefab;
    [SerializeField] GameObject sniperPrefab;


    public void generateWeapon(Vector3 position, Quaternion rotation)
    {
        float ran=UnityEngine.Random.value;
        if(ran<0.5f)
            generateStandardGun(position,rotation);
        if(ran>0.5f&&ran<0.75f)
            generateShotgun(position,rotation);
        if(ran>0.75f)
            generateSniper(position,rotation);
    }

    void generateStandardGun(Vector3 position, Quaternion rotation)
    {
        GameObject generatedWeapon=(GameObject)Instantiate(standardGunPrefab);
        float ran=UnityEngine.Random.value;
        generatedWeapon.GetComponent<Weapon>().name="Standard Gun";
        generatedWeapon.GetComponent<Weapon>().range=4.0f;
        ran*=4;
        ran+=1;
        generatedWeapon.GetComponent<Weapon>().atk=Mathf.FloorToInt(ran);
        ran=UnityEngine.Random.value;
        ran*=40;
        ran+=80;
        generatedWeapon.GetComponent<Weapon>().delay=Mathf.FloorToInt(ran);
    }

    void generateShotgun(Vector3 position, Quaternion rotation)
    {
        GameObject generatedWeapon=(GameObject)Instantiate(shotgunPrefab);
        float ran=UnityEngine.Random.value;
        generatedWeapon.GetComponent<Weapon>().name="Shotgun";
        generatedWeapon.GetComponent<Weapon>().range=4.0f;
        ran*=10;
        ran+=5;
        generatedWeapon.GetComponent<Weapon>().atk=Mathf.FloorToInt(ran);
        ran=UnityEngine.Random.value;
        ran*=120;
        ran+=300;
        generatedWeapon.GetComponent<Weapon>().delay=Mathf.FloorToInt(ran);
    }


    void generateSniper(Vector3 position, Quaternion rotation)
    {
        GameObject generatedWeapon=(GameObject)Instantiate(sniperPrefab);
        float ran=UnityEngine.Random.value;
        generatedWeapon.GetComponent<Weapon>().name="Sniper Rifle";
        generatedWeapon.GetComponent<Weapon>().range=30.0f;
        ran*=5;
        ran+=3;
        generatedWeapon.GetComponent<Weapon>().atk=Mathf.FloorToInt(ran);
        ran=UnityEngine.Random.value;
        ran*=60;
        ran+=150;
        generatedWeapon.GetComponent<Weapon>().delay=Mathf.FloorToInt(ran);
    }

}
