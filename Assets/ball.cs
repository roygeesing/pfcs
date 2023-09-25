using System;
using UnityEngine;

public class ball : MonoBehaviour
{
    private Vector3 v = new Vector3(0, 0, 0);
    private Vector3 a = new Vector3(0, -9.81f, 0);
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        transform.position += v * Time.deltaTime + 0.5f * Time.deltaTime * Time.deltaTime * a;
        
        v += a * Time.deltaTime;
        if (transform.position.y < 0)
        {
            v = -v;
        }
    }
}
