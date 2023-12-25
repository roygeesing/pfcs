using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Kollision : MonoBehaviour
{
    public Collider blueCube;
    public bool elastic;
    public float greenCubeSpeed;
    public float greenCubeMass;
    public float blueCubeSpeed;
    public float blueCubeMass;

    private readonly Vector3 _greenCubeForward = new(1, 0, 0);
    private readonly Vector3 _blueCubeForward = new(-1, 0, 0);

    private Vector3 _greenCubeVelocity;
    private Vector3 _blueCubeVelocity;

    // Start is called before the first frame update
    void Start()
    {
        _greenCubeVelocity = greenCubeSpeed * _greenCubeForward;
        _blueCubeVelocity = blueCubeSpeed * _blueCubeForward;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        var greenCubePosition = transform.position;
        var blueCubePosition = blueCube.transform.position;
        var distance = Math.Abs((greenCubePosition - blueCubePosition).magnitude);

        if (distance < 1)
        {
            // collision
            if (elastic)
            {
                // elastic collision
                var totalMass = greenCubeMass + blueCubeMass;
                var greenCubeNewSpeed = (greenCubeMass - blueCubeMass) / totalMass * greenCubeSpeed -
                                        2 * blueCubeMass / totalMass * blueCubeSpeed;
                var blueCubeNewSpeed = 2 * greenCubeMass / totalMass * greenCubeSpeed -
                                       (blueCubeMass - greenCubeMass) / totalMass * blueCubeSpeed;
                _greenCubeVelocity = greenCubeNewSpeed * -_greenCubeForward;
                _blueCubeVelocity = blueCubeNewSpeed * -_blueCubeForward;
            }
            else
            {
                // inelastic collision
                var impulse = greenCubeMass * greenCubeSpeed - blueCubeMass * blueCubeSpeed;
                var newVelocity = impulse / (greenCubeMass + blueCubeMass) * _greenCubeForward;
                _greenCubeVelocity = newVelocity;
                _blueCubeVelocity = newVelocity;
            }
        }

        transform.position = greenCubePosition + _greenCubeVelocity * Time.deltaTime;
        blueCube.transform.position = blueCubePosition + _blueCubeVelocity * Time.deltaTime;
    }
}