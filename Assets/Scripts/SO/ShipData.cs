using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShipData", menuName = "ScriptableObjects/ShipData")]

public class ShipData : ScriptableObject
{
    public bool isMoving;

    public float rotationPercent;
    public Vector3 rotationVector;
}