using UnityEngine;
using UnityEngine.InputSystem;

public class ShipMouseSprite : UIManager
{
    [SerializeField] RectTransform mainSpriteTransform;                 // UI element for ship's movement sprite
    [SerializeField] float maxDistanceFromCenter;                       // Max distance sprite can move from screen center

    private Vector2 mouseDelta;                                         // Delta for mouse movement, updated by input
    private Vector2 screenCenter;                                       // Center of the screen, used to calculate limits
    private Vector3 smoothedPosition;                                   // Smoothed position for the interpolated sprite

    void Start()
    {
        screenCenter = new Vector2(Screen.width, Screen.height) / 2;
        mainSpriteTransform.position = screenCenter;
        smoothedPosition = screenCenter;
    }
    void Update()
    {
        UpdateMainSpritePosition();
        UpdateShipDataRotation();
        UpdateSmoothedPosition();
    }
    public void OnMouseDelta(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }
    private void UpdateMainSpritePosition()
    {
        mainSpriteTransform.position += (Vector3)mouseDelta;

        // Calculate offset and clamp the sprite within the defined limit
        Vector2 offsetFromCenter = (Vector2)mainSpriteTransform.position - screenCenter;
        Vector2 clampedPosition = Vector2.ClampMagnitude(offsetFromCenter, maxDistanceFromCenter) + screenCenter;

        // Set the clamped position to the main sprite
        mainSpriteTransform.position = clampedPosition;
    }
    private void UpdateShipDataRotation()
    {
        // Calculate offset from center for the smoothed position
        Vector2 smoothedOffsetFromCenter = (Vector2)smoothedPosition - screenCenter;
        Vector2 normalizedDirection = smoothedOffsetFromCenter.normalized;

        // Update rotation vector in ShipData
        shipData.rotationVector.x = normalizedDirection.x;
        shipData.rotationVector.y = normalizedDirection.y;

        // Calculate and update the rotation percent from 0 to 1 based on distance
        float distancePercent = smoothedOffsetFromCenter.magnitude / maxDistanceFromCenter;
        shipData.rotationPercent = distancePercent;
    }
    private void UpdateSmoothedPosition()
    {
        smoothedPosition = Vector2.Lerp(smoothedPosition, mainSpriteTransform.position, Time.deltaTime);
    }
}