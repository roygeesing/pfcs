using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Gravitation : MonoBehaviour
{
    public Collider earth;

    private const float G = 6.67E-11f;
    private const float EarthMass = 5.972E24f;
    private const float CometMass = 7.539E7f;
    private const float GravityProductEarth = G * EarthMass;
    private const float GravityProductComet = G * CometMass;
    private const float RadiusFactor = 1E5f;
    private const float RandomAngle = 30f;

    private Vector3 _earthVelocity = Vector3.zero;
    private Vector3 _cometVelocity = Vector3.zero;

    private void Start()
    {
        var initialSpeed = Random.Range(0, 20);
        var direction = (earth.transform.position - transform.position).normalized;
        var randomDirection =
            Quaternion.Euler(0, Random.Range(-RandomAngle, RandomAngle), Random.Range(-RandomAngle, RandomAngle))
            * direction;
        _cometVelocity = initialSpeed * randomDirection;
    }

    private void FixedUpdate()
    {
        // leapfrog step 1
        var halfTime = Time.deltaTime / 2;
        var earthPositionBetween = earth.transform.position + _earthVelocity * halfTime;
        var cometPositionBetween = transform.position + _cometVelocity * halfTime;

        // leapfrog step 2
        var earthToComet = cometPositionBetween - earthPositionBetween;
        var cometToEarth = -earthToComet;
        var radius = earthToComet.magnitude * RadiusFactor;
        var radiusSquared = (float)Math.Pow(radius, 2);
        var earthGravityForce = GravityProductComet / radiusSquared * earthToComet.normalized;
        var cometGravityForce = GravityProductEarth / radiusSquared * cometToEarth.normalized;

        _earthVelocity += earthGravityForce * halfTime;
        _cometVelocity += cometGravityForce * halfTime;

        // leapfrog step 3
        earth.transform.position = earthPositionBetween + _earthVelocity * halfTime;
        transform.position = cometPositionBetween + _cometVelocity * halfTime;
    }
}