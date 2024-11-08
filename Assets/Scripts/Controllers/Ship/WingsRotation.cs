using UnityEngine;
using UnityEngine.InputSystem;

public class WingsRotation : MonoBehaviour
{
    public Transform wings;

    private bool isRotating;
    public float rotationSpeed;
    private float rotationValue;
    private float currentRotationValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (isRotating)
        {
            currentRotationValue = Mathf.Lerp(currentRotationValue, rotationValue, 2f * Time.deltaTime);
        }
        else
        {
            currentRotationValue = Mathf.Lerp(currentRotationValue, 0, 2f * Time.deltaTime);
        }
        wings.rotation *= Quaternion.Euler(0, 0, currentRotationValue * rotationSpeed);
    }
    public void OnRotatewings(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isRotating = true;
        }
        else if (context.canceled)
        {
            isRotating = false;
        }
        rotationValue = context.ReadValue<Vector3>().x;
    }
}