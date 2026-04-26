using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;


//using System.Numerics;
using UnityEngine;

public class smoothstepTween : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private Vector3 a;
    [SerializeField] private Vector3 b;

    [SerializeField] float speed;
    IEnumerator MoveRoutine()
    {
        float t = 0.0f;
        while(t < 1.0f)
        {
            float g = Mathf.SmoothStep(0.0f, 1.0f, t);
            this.transform.position = Vector3.Lerp(this.a,this.b,g);

            t+= Time.deltaTime * this.speed;
            yield return null;
        }
this.transform.position = this.b;

    }
    void  Start()
    {
         this.StartCoroutine(this.MoveRoutine());
        
    }

    // Update is called once per frame

}
