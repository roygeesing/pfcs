using System;
using UnityEngine;

public class FreierFall : MonoBehaviour
{
    private const float RotationSpeed = 20f;
    private readonly Vector3 _rotateUp = new(RotationSpeed, 0, 0);
    private readonly Vector3 _rotateLeft = new(0, 0, RotationSpeed);
    private readonly Vector3 _gravity = new(0, -9.81f, 0);
    
    private const float Mass = 2f;
    private const float AirDensity = 0.1225f;
    private const float DragCoefficient = 0.47f;
    private const float Area = (float)(0.5f * 0.5f * Math.PI);
    
    private Vector3 _v = new(0, 0, 0);

    public Collider plane;

    private void FixedUpdate()
    {
        var weightForce = Mass * _gravity;
        
        var velocity = _v.magnitude;
        var airResistance = 0.5f * DragCoefficient * AirDensity * Area * velocity * velocity;
        var airResistanceForce = -_v.normalized * airResistance;
        
        var acceleration = weightForce + airResistanceForce;
        transform.position += Time.deltaTime * _v + 0.5f * Time.deltaTime * Time.deltaTime * acceleration;
        _v += acceleration * Time.deltaTime;

        var normal = plane.transform.up;
        var vector = plane.transform.position - transform.position;
        var distanceVector = Vector3.Project(vector, normal);
        var distance = distanceVector.magnitude;
        if (distance < 0.5f)
        {
            _v = Vector3.Reflect(_v, normal);
        }

        var up = Time.deltaTime * _rotateUp;
        var left = Time.deltaTime * _rotateLeft;
        if (Input.GetKey("up") || Input.GetKey("w"))
        {
            plane.transform.Rotate(up);
        }
        if (Input.GetKey("down") || Input.GetKey("s"))
        {
            plane.transform.Rotate(-up);
        }
        if (Input.GetKey("left") || Input.GetKey("a"))
        {
            plane.transform.Rotate(left);
        }
        if (Input.GetKey("right") || Input.GetKey("d"))
        {
            plane.transform.Rotate(-left);
        }
    }
}
