using System;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("User Inputs")]
    public float inputSensitivity = 1.0f;
    public KeyCode brakeKey = KeyCode.Space;

    [Header("Wheel Colliders and Transforms")]
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    [Header("Car Physics Parameters")]
    [SerializeField, Range(0f, 100f)] private float maxSteeringAngle = 30f;
    [SerializeField, Range(0f, 1000f)] private float motorForce = 50f;
    [SerializeField] private float brakeForce = 0f;

    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    private bool isBreaking;

    private void Start()
    {
        CheckComponents();
    }

    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal") * inputSensitivity;
        verticalInput = Input.GetAxis("Vertical") * inputSensitivity;
        isBreaking = Input.GetKey(brakeKey);
    }

    private void HandleSteering()
    {
        steerAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;

        brakeForce = isBreaking ? 3000f : 0f;
        frontLeftWheelCollider.brakeTorque = brakeForce;
        frontRightWheelCollider.brakeTorque = brakeForce;
        rearLeftWheelCollider.brakeTorque = brakeForce;
        rearRightWheelCollider.brakeTorque = brakeForce;
    }

    private void UpdateWheels()
    {
        UpdateWheelPos(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPos(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheelPos(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateWheelPos(rearRightWheelCollider, rearRightWheelTransform);
    }

    private void UpdateWheelPos(WheelCollider wheelCollider, Transform trans)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        trans.rotation = rot;
        trans.position = pos;
    }

    private void CheckComponents()
    {
        if (frontLeftWheelCollider == null || frontRightWheelCollider == null ||
            rearLeftWheelCollider == null || rearRightWheelCollider == null ||
            frontLeftWheelTransform == null || frontRightWheelTransform == null ||
            rearLeftWheelTransform == null || rearRightWheelTransform == null)
        {
            Debug.LogError("One or more WheelCollider or Transform components are missing!");
        }
    }
}
