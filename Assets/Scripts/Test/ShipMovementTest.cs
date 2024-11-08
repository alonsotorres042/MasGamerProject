using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMovementTest : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Movement Settings")]
    public float baseSpeed = 10f; // Velocidad base
    public float maxSpeed = 100f; // Velocidad m�xima
    public float acceleration = 2f; // Aceleraci�n progresiva
    public float deceleration = 2f; // Desaceleraci�n cuando se suelta el input
    public float boostMultiplier = 3f; // Multiplicador de velocidad para el impulso

    [Header("Rotation Settings")]
    public float pitchSpeed = 90f; // Velocidad de rotaci�n en el eje X (arriba/abajo)
    public float yawSpeed = 90f;   // Velocidad de rotaci�n en el eje Y (izquierda/derecha)
    public float rollSpeed = 90f;  // Velocidad de rotaci�n en el eje Z (girar lateralmente)

    private float currentSpeed;
    private Vector2 rotationInput; // Input para la rotaci�n (pitch/yaw)
    private float rollInput;       // Input para la rotaci�n en roll (Z)
    private Vector3 moveInput;     // Input para el movimiento en el espacio
    private bool isBoosting;       // Indica si el boost est� activado

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = baseSpeed;
    }

    private void Update()
    {
        HandleRotation();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    // M�todo para manejar la rotaci�n de la nave
    private void HandleRotation()
    {
        // Rotaci�n Pitch (X) y Yaw (Y) usando el input del jugador
        transform.Rotate(Vector3.right, rotationInput.y * pitchSpeed * Time.deltaTime); // Pitch (arriba/abajo)
        transform.Rotate(Vector3.up, rotationInput.x * yawSpeed * Time.deltaTime);      // Yaw (izquierda/derecha)

        // Aplicar Roll (girar lateralmente)
        transform.Rotate(Vector3.forward, -rollInput * rollSpeed * Time.deltaTime);     // Roll (girar hacia los lados)
    }

    // M�todo para manejar el movimiento de la nave
    private void HandleMovement()
    {
        // Aumentar o reducir la velocidad seg�n el input del jugador y si est� activado el boost
        float targetSpeed = isBoosting ? maxSpeed * boostMultiplier : baseSpeed;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

        Vector3 movement = transform.TransformDirection(moveInput) * currentSpeed * Time.fixedDeltaTime;

        rb.velocity = Vector3.Lerp(rb.velocity, movement, deceleration * Time.fixedDeltaTime);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector3>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        rotationInput = context.ReadValue<Vector2>();
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        rollInput = context.ReadValue<float>();
    }

    public void OnBoost(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isBoosting = true;
        }
        else if (context.canceled)
        {
            isBoosting = false;
        }
    }
}