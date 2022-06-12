using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickupGeneration : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject healthPickingPrefab;

    public GameObject generatePickup(Vector3 position, Quaternion rotation)
    {
        GameObject generatedPickup = Instantiate(healthPickingPrefab, position, rotation);
        float ran=UnityEngine.Random.value;
        ran*=20;
        ran+=10;
        generatedPickup.GetComponent<HealthPickupBehaviour>().hp=Mathf.FloorToInt(ran);
        return generatedPickup;
    }
}
