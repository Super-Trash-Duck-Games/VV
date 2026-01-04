using System;
using UnityEngine;

[Serializable]
public class MovementPackage : MonoBehaviour
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
    [Header("Kumkum")]
    public Vector2 wallJumpForce;
    public float specialHangTime;
    public float specialStompForce;
    [Header("Gargantuar")]
    public float punchDash;
    public float dashDelay;
    public float hitDuration;
    [Header("Chameleon")]
    public float grappleDetectionLenght;
    public float adjustDistanceRate;
    public float rbLinearDamplingWhileGrappled;
    public float tongueExtensionSpeed;
}