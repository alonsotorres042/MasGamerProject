using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public float lookSpeed = 2f; // Sensibilidad de la cámara
    public Transform cameraTransform; // La cámara que sigue al jugador

    private float rotationX = 0f;

    void Update()
    {
        // Control de la cámara con el mouse
        float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
        float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;
        Debug.Log(Input.GetAxis("Mouse X") + " ; " + Input.GetAxis("Mouse Y"));

        // Rotar la cámara en el eje X (vertical)
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Limitar la rotación vertical
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        // Rotar el personaje en el eje Y (horizontal)
        transform.Rotate(Vector3.up * mouseX);
    }
}