using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // ورودی‌های کاربر به خودرو
    private float horizontalInput;
    private float verticalInput;
    private float steerAngle;
    private bool isBreaking;

    // کولای‌ها و ترانسفورم‌های چرخ‌ها
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;
    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    // حداکثر زاویه چرخش و نیروی موتور و نیروی ترمز
    public float maxSteeringAngle = 30f;
    public float motorForce = 50f;
    public float brakeForce = 0f;

    // تابعی که در هر فریم فراخوانی می‌شود
    private void FixedUpdate()
    {
        GetInput();         // گرفتن ورودی‌های کاربر
        HandleMotor();      // مدیریت نیروی موتور و ترمز
        HandleSteering();   // مدیریت زاویه چرخش
        UpdateWheels();     // به‌روزرسانی ترانسفورم چرخ‌ها
    }

    // گرفتن ورودی‌های کاربر
    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    // مدیریت زاویه چرخش
    private void HandleSteering()
    {
        steerAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;
    }

    // مدیریت نیروی موتور و ترمز
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

    // به‌روزرسانی ترانسفورم چرخ‌ها
    private void UpdateWheels()
    {
        UpdateWheelPos(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPos(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheelPos(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateWheelPos(rearRightWheelCollider, rearRightWheelTransform);
    }

    // به‌روزرسانی موقعیت و چرخش یک چرخ
    private void UpdateWheelPos(WheelCollider wheelCollider, Transform trans)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        trans.rotation = rot;
        trans.position = pos;
    }
}
