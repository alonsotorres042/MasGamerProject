using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class ShipRotation : MonoBehaviour
{
    [Header("Ship Data Reference")]
    [SerializeField] ShipData shipData;                       // Reference to the ship data

    [Header("Rotation Settings")]
    [SerializeField] Transform rotationTarget;                // Target object to rotate
    [SerializeField] float maxRotationSpeed;                  // Maximum rotation speed
    [SerializeField] float accelerationTime;                  // Time to reach full rotation speed
    [SerializeField] float decelerationTime;                  // Time to reach zero rotation speed

    [Header("Z Rotation Settings")]
    [SerializeField] float maxZRotationSpeed;                 // Maximum Z rotation speed
    [SerializeField] float ZAccelerationTime;                 // Time to reach full Z rotation speed
    [SerializeField] float ZDecelerationTime;                 // Time to reach zero Z rotation speed

    private float rotationIntensity;                          // Current intensity (0 to 1) for controlling acceleration/deceleration
    private float ZRotationIntensity;                          // Current intensity (0 to 1) for controlling Z acceleration/deceleration
    private Tween currentTween;                               // Tween to smoothly adjust rotation intensity
    void FixedUpdate()
    {
        ApplyRotation();
    }
    public void OnSetIntensity(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentTween?.Kill();
            SetIntensity(1f, accelerationTime);
        }
        else if (context.canceled)
        {
            currentTween?.Kill();
            SetIntensity(0f, decelerationTime);
        }
    }
    public void OnZRotation(InputAction.CallbackContext context)
    {
        float ZRotationValue = context.ReadValue<float>();
        if (ZRotationIntensity != 0)
        {
            shipData.rotationVector.z = ZRotationValue;
        }

        if (context.performed)
        {
            currentTween?.Kill();
            SetZIntensity(ZRotationValue, ZAccelerationTime);
        }
        else if (context.canceled)
        {
            currentTween?.Kill();
            SetZIntensity(0f, ZDecelerationTime);
        }
    }
    public void SetZIntensity(float targetZIntensity, float duration)
    {
        currentTween?.Kill();
        currentTween = DOTween.To(() => ZRotationIntensity, x => ZRotationIntensity = x, targetZIntensity, duration).SetEase(Ease.InOutCubic).OnKill(() => currentTween = null);
    }
    public void SetIntensity(float targetIntensity, float duration)
    {
        currentTween?.Kill();
        currentTween = DOTween.To(() => rotationIntensity, x => rotationIntensity = x, targetIntensity, duration).SetEase(Ease.InOutCubic).OnKill(() => currentTween = null);
    }
    public void ApplyRotation()
    {
        Vector2 rotationVector = new Vector3(-shipData.rotationVector.y, shipData.rotationVector.x);
        Vector2 rotationDelta = rotationVector * shipData.rotationPercent * maxRotationSpeed * rotationIntensity * Time.deltaTime;
        float ZRotationDelta = shipData.rotationVector.z * maxZRotationSpeed * ZRotationIntensity * Time.deltaTime;
        rotationTarget.Rotate(rotationDelta.x, rotationDelta.y, 0.1f);
    }
}