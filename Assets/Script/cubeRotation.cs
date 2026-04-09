using System.Diagnostics;
using System.Numerics;
using UnityEngine;

public class cubeRotation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UnityEngine.Debug.Log("HelloWorld");
        UnityEngine.Debug.Log("GoodbyeWorld");
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position += new UnityEngine.Vector3(1f,0f,0f) ;
        
    }
}
