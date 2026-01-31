using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public float m_Speed = 5f;
    public float m_BlockSpeed = 4f;
    public float lookSpeedX = 2.5f;
    public float lookSpeedY = 2.5f;
    public float dashSpeed = 50.0f;
    public float dashCooldown = 1.0f;

    public Transform playerCamera;
    public Animator animator;
    public CinemachineImpulseSource impulseSource;
    public bool canBlock = false;
    public bool blocking = false;

    Rigidbody m_Rigidbody;
    Vector3 movementVector = Vector3.zero;
    float dashCooldownTimer = 0.0f;
    bool grounded = false;
    

    void Start()
    {
        //Fetch the Rigidbody from the GameObject with this script attached
        m_Rigidbody = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 bodyLook = new Vector3(0, Input.GetAxis("Mouse X"), 0);
        Vector3 cameraLook = new Vector3(-Input.GetAxis("Mouse Y"), 0, 0);

        transform.Rotate(bodyLook * lookSpeedY);
        playerCamera.Rotate(cameraLook * lookSpeedX);

        //animator.SetFloat("LookX", Input.GetAxis("Mouse X"));
        //animator.SetFloat("LookY", Input.GetAxis("Mouse Y"));

        //if (Input.GetButtonDown("Jump") && groundedPlayer)
        //{
        //    playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityValue);
        //}

        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        //float smoothedMove = Mathf.SmoothDamp(transform.position.y, verticalMove, ref yVelocity, smoothTime);

        animator.SetFloat("HorizontalMove", horizontalMove, 0.5f, Time.deltaTime);
        animator.SetFloat("VerticalMove", verticalMove);

        movementVector = (transform.forward * verticalMove + transform.right * horizontalMove);

        if (movementVector != Vector3.zero)
        {
            animator.SetBool("PlayerMoving", true);
        }
        else {
            animator.SetBool("PlayerMoving", false);
        }

        if(Input.GetButtonDown("Jump") && canBlock)
        {
            animator.SetBool("Blocking", true);
            animator.ResetTrigger("Light Attack");
            animator.ResetTrigger("Heavy Attack");
            blocking = true;
        }

        if (Input.GetButtonUp("Jump") && canBlock)
        {
            animator.SetBool("Blocking", false);
            animator.ResetTrigger("Light Attack");
            animator.ResetTrigger("Heavy Attack");
            blocking = false;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Light Attack");
        }

        if (Input.GetButtonDown("Fire2"))
        {
            animator.SetTrigger("Heavy Attack");
        }

        if (Input.GetButtonDown("Dash") && dashCooldownTimer <= 0)
        {
            animator.SetTrigger("Dash");
            m_Rigidbody.AddForce(movementVector * dashSpeed);
            //impulseSource.GenerateImpulse(0.2f);
            dashCooldownTimer = dashCooldown;
        }

        if (dashCooldownTimer > 0) {
            dashCooldownTimer -= Time.deltaTime;
        }

        if (!grounded) {
            m_Rigidbody.AddForce(Vector3.up * -5.0f);
        }
    }

    void FixedUpdate()
    {
        //Store user input as a movement vector
        //Vector3 m_Input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));





        //Apply the movement vector to the current position, which is
        //multiplied by deltaTime and speed for a smooth MovePosition
        if (!blocking)
        {
            m_Rigidbody.MovePosition(transform.position + movementVector * m_Speed * Time.fixedDeltaTime);
        }
        else {
            m_Rigidbody.MovePosition(transform.position + movementVector * m_BlockSpeed * Time.fixedDeltaTime);
        }
    }

    public void ResetAttackTriggers()
    {
        animator.ResetTrigger("Light Attack");
        animator.ResetTrigger("Heavy Attack");
    }

    //public void LightAttack() {
    //    bool m_HitDetect;

    //    m_HitDetect = Physics.BoxCast(m_Collider.bounds.center, transform.localScale * 0.5f, transform.forward, out m_Hit, transform.rotation, 1.0f);
    //    if (m_HitDetect)
    //    {
    //        //Output the name of the Collider your Box hit
    //        Debug.Log("Hit : " + m_Hit.collider.name);
    //    }
    //}

    public void UnlockBlock() {
        canBlock = true;
    }

    void OnCollisionStay(Collision other)
    {
        grounded = true;
    }

    void OnCollisionExit(Collision other)
    {
        grounded = false;
    }
}
