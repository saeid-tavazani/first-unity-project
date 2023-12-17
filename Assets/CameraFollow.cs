using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // آفست، هدف، سرعت ترجمه و سرعت چرخش دوربین
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform target;
    [SerializeField] private float translateSpeed;
    [SerializeField] private float rotationSpeed;

    // تابع فیکس شده که در هر فریم فراخوانی می‌شود
   private void FixedUpdate()
{
    if (target == null)
        return;

    HandleTranslation();
    HandleRotation();
}

   
    // مدیریت ترجمه دوربین
   private void HandleTranslation()
{
    if (target == null)
        return;

    var targetPosition = target.TransformPoint(offset);
    transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
}

    
    // مدیریت چرخش دوربین
  private void HandleRotation()
{
    if (target == null)
        return;

    var direction = target.position - transform.position;
    var rotation = Quaternion.LookRotation(direction, Vector3.up);
    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
}

}
