using System;
using UnityEngine;

public class RutschenderEisblock : MonoBehaviour
{
    private readonly Vector3 _center = new(0, 0, 0);
    private const float RotationSpeed = 20f;
    private readonly Vector3 _rotateUp = new(RotationSpeed, 0, 0);
    private readonly Vector3 _rotateLeft = new(0, 0, RotationSpeed);
    private readonly Vector3 _gravity = new(0, -9.81f, 0);

    private const float Mass = 2f;
    private const float StaticFriction = 0.5f;
    private const float DynamicFriction = 0.3f;
    private const float AirDensity = 0.1225f;
    private const float DragCoefficient = 1.05f;
    private const float Area = (float)(1f * 1f * Math.PI);
    
    private Vector3 _v = new(0, 0, 0);
    
    public Collider plane;
    
    private void FixedUpdate()
    {
        var weightForce = Mass * _gravity;
        var normal = plane.transform.up;
        var normalForce = -Vector3.Project(weightForce, normal);
        
        var velocity = _v.magnitude;
        var airResistance = 0.5f * DragCoefficient * AirDensity * Area * velocity * velocity;
        var airResistanceForce = -_v.normalized * airResistance;

        var acceleration = weightForce + normalForce + airResistanceForce;
        
        var isMoving = _v.magnitude > 0.001;
        var friction = isMoving ? DynamicFriction : StaticFriction;   
        var frictionForce = -_v.normalized * (normalForce.magnitude * friction);
        
        if (!isMoving && frictionForce.magnitude > acceleration.magnitude)
        {
            acceleration = Vector3.zero;
        }
        else
        {
            acceleration += frictionForce;
        }
        
        transform.position += Time.deltaTime * _v + 0.5f * Time.deltaTime * Time.deltaTime * acceleration;
        _v += acceleration * Time.deltaTime;
        
        var up = Time.deltaTime * _rotateUp;
        var left = Time.deltaTime * _rotateLeft;
        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            plane.transform.RotateAround(_center, up, 1);
            transform.RotateAround(_center, up, 1);
            _v = Quaternion.AngleAxis(1, up) * _v;
        }
        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            plane.transform.RotateAround(_center, -up, 1);
            transform.RotateAround(_center, -up, 1);
            _v = Quaternion.AngleAxis(1, -up) * _v;
        }
        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            plane.transform.RotateAround(_center, left, 1);
            transform.RotateAround(_center, left, 1);
            _v = Quaternion.AngleAxis(1, left) * _v;
        }
        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            plane.transform.RotateAround(_center, -left, 1);
            transform.RotateAround(_center, -left, 1);
            _v = Quaternion.AngleAxis(1, -left) * _v;
        }
    }
}
