using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float m_Speed = 5f;

    public Transform playerCamera;
    public Animator animator;

    Rigidbody m_Rigidbody;
    Vector3 movementVector = Vector3.zero;

    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 bodyLook = new Vector3(0, Input.GetAxis("Mouse X"), 0);
        Vector3 cameraLook = new Vector3(-Input.GetAxis("Mouse Y"), 0, 0);

        transform.Rotate(bodyLook);
        playerCamera.Rotate(cameraLook);

        //animator.SetFloat("LookX", Input.GetAxis("Mouse X"));
        //animator.SetFloat("LookY", Input.GetAxis("Mouse Y"));

        //if (Input.GetButtonDown("Jump") && groundedPlayer)
        //{
        //    playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
        //}

        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        animator.SetFloat("HorizontalMove", horizontalMove);
        animator.SetFloat("VerticalMove", verticalMove);

        movementVector = (transform.forward * verticalMove + transform.right * horizontalMove);

        if (movementVector != Vector3.zero)
        {
            animator.SetBool("PlayerMoving", true);
        }
        else {
            animator.SetBool("PlayerMoving", false);
        }
    }

    void FixedUpdate()
    {
        //Store user input as a movement vector
        //Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        



        //Apply the movement vector to the current position, which is
        //multiplied by deltaTime and speed for a smooth MovePosition
        m_Rigidbody.MovePosition(transform.position + movementVector * m_Speed * Time.fixedDeltaTime);
    }
}
