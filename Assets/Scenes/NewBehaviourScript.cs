using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    bool isJumping;
    // Start is called before the first frame update
    void Start()
    {
        isJumping=false;
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb=GetComponent<Rigidbody>();
        
        float distanceX = 10 * Time.deltaTime * Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * distanceX);
        
        float distanceY = 10 * Time.deltaTime * Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * distanceY);

        float mouseXvalue =Input.GetAxis("Mouse X");
        float mouseYvalue =Input.GetAxis("Mouse Y");
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0,mouseXvalue,0));
        rb.MoveRotation(rb.rotation*deltaRotation);

        if(Input.GetKey("space")){
            if(isJumping==false){
                isJumping=true;
                rb.AddForce(new Vector3(0,100,0));
            }
        }
        if(rb.velocity.y==0){
            isJumping=false;
        }
    }

}
