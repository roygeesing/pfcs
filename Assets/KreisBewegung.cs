using UnityEngine;

public class KreisBewegung : MonoBehaviour
{
    private readonly Vector3 _center = new(0, 0, 0);
    
    private Vector3 _v = new(20, 0, 0);

    private void FixedUpdate()
    {
        var halfTime = Time.deltaTime / 2;
        var velocitySquared = _v.magnitude * _v.magnitude;
        
        var positionBetween = transform.position + halfTime * _v;
        
        var inwards = _center - positionBetween;
        var inwardsForce = velocitySquared / inwards.magnitude;
        var acceleration = inwards.normalized * inwardsForce;
        _v += acceleration * Time.deltaTime;
        transform.position = positionBetween + halfTime * _v;
    }
}
