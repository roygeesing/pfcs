using UnityEngine;

public class FreierFall : MonoBehaviour
{
    private Vector3 _v = new(0, 0, 0);
    private readonly Vector3 _a = new(0, -9.81f, 0);

    private const float RotationSpeed = 20f;
    private readonly Vector3 _rotateUp = new(RotationSpeed, 0, 0);
    private readonly Vector3 _rotateLeft = new(0, 0, RotationSpeed);

    public Collider plane;

    private void FixedUpdate()
    {
        transform.position += Time.deltaTime * _v + 0.5f * Time.deltaTime * Time.deltaTime * _a;
        _v += _a * Time.deltaTime;

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
