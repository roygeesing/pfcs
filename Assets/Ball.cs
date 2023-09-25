using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector3 _v = new(0, 0, 0);
    private readonly Vector3 _a = new(0, -9.81f, 0);

    private Collision _collision;

    private void FixedUpdate()
    {
        if (_collision != null)
        {
            _v = Vector3.Reflect(_v, _collision.contacts[0].normal);
            _collision = null;
        }
        
        transform.position += Time.deltaTime * _v + 0.5f * Time.deltaTime * Time.deltaTime * _a;
        
        _v += _a * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        _collision = other;
    }
}
