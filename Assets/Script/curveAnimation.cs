using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class curveAnimation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform a;
    [SerializeField] private Transform b;

    [SerializeField] private float speed;

    [SerializeField] private AnimationCurve curve;


IEnumerator Animate()
{
    float timer = 0.0f;
    while (timer < 1.0f)
    {
        var position = Vector3.Lerp(this.a.position, this.b.position,timer);
        position.y = this.curve.Evaluate(timer);

        this.transform.position = position;

        yield return null;
        timer += Time.deltaTime * this.speed;
    }
    float offsetY = this.curve.Evaluate(1.0f);
    this.transform.position = b.position;
}

IEnumerator Start()
    {
        yield return this.StartCoroutine(Animate());
    }
}
