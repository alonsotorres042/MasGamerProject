using UnityEngine;
using UnityEngine.InputSystem;
public class TransformRotateTests : UIManager
{
    public Transform Victim;
    public float RVelocMulty;
    public Vector3 rotateVector;

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rotateVector = new Vector3(-shipData.rotationVector.y, shipData.rotationVector.x, shipData.rotationVector.z);
            Victim.Rotate(rotateVector * Time.deltaTime * RVelocMulty);
        }
    }
}