using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public float lookSpeed = 2f; // Sensibilidad de la c�mara
    public Transform cameraTransform; // La c�mara que sigue al jugador

    private float rotationX = 0f;

    void Update()
    {
        // Control de la c�mara con el mouse
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;
        Debug.Log(Input.GetAxis("Mouse X") + " ; " + Input.GetAxis("Mouse Y"));

        // Rotar la c�mara en el eje X (vertical)
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Limitar la rotaci�n vertical
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        // Rotar el personaje en el eje Y (horizontal)
        transform.Rotate(Vector3.up * mouseX);
    }
}