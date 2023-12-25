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
        HandleTranslation(); // مدیریت ترجمه دوربین
        HandleRotation();    // مدیریت چرخش دوربین
    }
   
    // مدیریت ترجمه دوربین
    private void HandleTranslation()
    {
        // محاسبه موقعیت مطلوب دوربین بر اساس موقعیت هدف و آفست
        var targetPosition = target.TransformPoint(offset);
        
        // استفاده از تابع Lerp برای انجام ترجمه با سرعت معین
        transform.position = Vector3.Lerp(transform.position, targetPosition, translateSpeed * Time.deltaTime);
    }
    
    // مدیریت چرخش دوربین
    private void HandleRotation()
    {
        // محاسبه جهت مورد نظر بر اساس موقعیت هدف و موقعیت فعلی دوربین
        var direction = target.position - transform.position;
        
        // محاسبه چرخش مطلوب با استفاده از LookRotation
        var rotation = Quaternion.LookRotation(direction, Vector3.up);
        
        // استفاده از تابع Lerp برای انجام چرخش با سرعت معین
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }
}
