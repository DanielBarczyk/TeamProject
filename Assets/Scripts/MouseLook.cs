    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 100.0f;
    public Transform playerBody;
    private float xRotation = 0.0f;
    public Ray ray;
    public float rayDistance = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        if(!this.gameObject.GetComponentInParent<Inventory>().visible)
        {
            Cursor.visible=false;
            
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

            transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
            playerBody.Rotate(Vector3.up * mouseX);

            ray = new Ray(transform.position, transform.forward);
            Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);
        }
        else
        {
            Cursor.lockState=CursorLockMode.Confined;
            Cursor.visible=true;
        }
    }
}
