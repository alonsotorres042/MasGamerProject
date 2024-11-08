using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class ShipMovement : MonoBehaviour
{
    [Header("Ship Data Reference")]
    [SerializeField] ShipData shipData;           // Reference to the ship data
    
    [Header("Rotation Settings")]
    [SerializeField] Transform forwardReference;  // Transform used to determine the forward direction of the ship
    [SerializeField] float baseSpeed;             // Base speed of the ship
    [SerializeField] float sprintMultiplier;      // Multiplier for speed when sprinting
    [SerializeField] float sprintAccelerationTime;      // Time required to accelerate to sprint speed
    [SerializeField] float sprintDecelerationTime;      // Time required to decelerate back to base speed

    private float currentSpeed;                   // Current speed of the ship, which changes during sprinting
    private Rigidbody rb;                         // Reference to the Rigidbody for physics-based movement
    private Tween currentTween;                   // Current Tween controlling speed transitions
    private bool isShipMoving                     // Property that controls whether the ship is moving or not
    { 
        get => shipData.isMoving; 
        set => shipData.isMoving = value;
    }                 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = baseSpeed;
    }
    void FixedUpdate()
    {
        if (isShipMoving)
        {
            rb.velocity = forwardReference.TransformDirection(Vector3.forward) * currentSpeed;
        }
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isShipMoving = true;
        }
        else if (context.canceled)
        {
            isShipMoving = false;
        }
    }
    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentTween?.Kill();
            SetSpeed(baseSpeed * sprintMultiplier, sprintAccelerationTime);
        }
        else if (context.canceled)
        {
            currentTween?.Kill();
            SetSpeed(baseSpeed, sprintDecelerationTime);
        }
    }
    private void SetSpeed(float targetSpeed, float duration)
    {
        currentTween?.Kill();
        currentTween = DOTween.To(() => currentSpeed, x => currentSpeed = x, targetSpeed, duration).SetEase(Ease.InOutSine).OnKill(() => currentTween = null);
    }
}