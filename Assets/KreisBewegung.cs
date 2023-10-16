using UnityEngine;

public class KreisBewegung : MonoBehaviour
{
    private Vector3 _v = new(20, 0, 0);
    private readonly Vector3 _center = new(0, 0, 0);
    
    private void FixedUpdate()
    {
        var thisTransform = transform;
        var position = thisTransform.position;
        var halfTime = Time.deltaTime / 2;
        var vMagnitudeSquared = _v.magnitude * _v.magnitude;
        
        var positionBetween = position + halfTime * _v;
        
        var inwards = _center - positionBetween;
        var inwardsForce = vMagnitudeSquared / inwards.magnitude;
        var acceleration = inwards.normalized * inwardsForce;
        _v += acceleration * Time.deltaTime;
        thisTransform.position = positionBetween + halfTime * _v;
    }
}
