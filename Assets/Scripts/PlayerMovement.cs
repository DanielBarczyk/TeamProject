using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController characterController;
    public float movementSpeed = 5.0f;
    public float jumpHeight = 3.0f;
    public float gravity = -19.62f;
    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;
    private Vector3 velocity;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate=60;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0) {
            velocity.y = -2.0f;
        }

        float offsetX = Input.GetAxis("Horizontal");
        float offsetZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * offsetX + transform.forward * offsetZ;

        characterController.Move(move * movementSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);


        if(Input.GetKey("r")){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Debug.Log("Reloaded\n");
        }
    }

}
