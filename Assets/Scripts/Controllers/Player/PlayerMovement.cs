using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed = 5f;

    private Rigidbody rb;
    private Vector2 inputVector;
    private Vector3 moveDirection;

    public float jumpForce = 5f;
    public float raycastDistance = 1.2f;
    private bool CanJump = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        Vector3 move = transform.right * inputVector.x + transform.forward * inputVector.y;
        moveDirection = move * MovementSpeed;

        Vector3 velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
        rb.velocity = velocity;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, raycastDistance))
        {
            CanJump = true;
        }
        else
        {
            CanJump = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        inputVector = context.ReadValue<Vector2>();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && CanJump)
        {
            rb.AddForce(transform.TransformDirection(Vector3.up) * jumpForce, ForceMode.Impulse);
        }
    }
}