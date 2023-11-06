using UnityEngine;

public class Feder : MonoBehaviour
{
    private const float SpringNormalLength = 1.6f;
    private const float SpringConstant = 100f;
    
    private Vector3 _v = new(0, 0, 0);
    
    public GameObject spring;

    void FixedUpdate()
    {
        var halfTime = Time.deltaTime / 2;

        var betweenMovement = halfTime * _v;
        var positionBetween = transform.position + betweenMovement;
        var springPositionBetween = spring.transform.position + betweenMovement / 2;
        var springScaleBetween = spring.transform.localScale - betweenMovement / 2;

        var stretchAmount = SpringNormalLength - springScaleBetween.y;
        var springForce = spring.transform.up * (-SpringConstant * stretchAmount);
        var acceleration = springForce;

        _v += acceleration * Time.deltaTime;
        //var movement = Time.deltaTime * _v + 0.5f * Time.deltaTime * Time.deltaTime * a;
        var movement = halfTime * _v;
        transform.position = positionBetween + movement;
        spring.transform.position = springPositionBetween + movement / 2;
        spring.transform.localScale = springScaleBetween - movement / 2;
    }
}
