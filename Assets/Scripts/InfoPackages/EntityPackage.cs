using System;
using UnityEngine;

[Serializable]
public class EntityPackage : MonoBehaviour
{
    [Header("Movement")]
    public float speed;
    public float runSpeed;
    public float turnSpeed;
    //[Range(.1f, 10f)]
    //public float acceleration;
    [Range(.1f, 10f)]
    public float deceleration;
    [Header("Air")]
    public float airControl;
    public bool doubleJump;
    [Header("Jump")]
    public float weight;
    public float jumpForce;
    public float jumpCutoff;
}