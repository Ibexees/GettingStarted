using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG;
using System.Collections;

public class doTween : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private List<Vector3> positions;
    [SerializeField] private List<float> movingTimes;
    [SerializeField] private float waitingTime;

    private bool isPlaying = false;
    private Sequence sequence;
    private Vector3 lastPosition;

    /* Idee damit character auf platform bleibt
    public Vector3 GetVelocity()
    {
        Vector3 velocity = Vector3.Distance(this.transform.position, this.lastPosition) / Time.deltaTime;
        lastPosition = this.transform.position;

        return velocity
    }*/
    private void CreateSequence()
    {
        this.sequence = DOTween.Sequence();

        for(int i = 0; i < this.positions.Count; i++)
        {
            var tween = this.transform.DOMove(this.positions[i], this.movingTimes[i]);
            tween.SetEase(Ease.InOutQuint);
            this.sequence.Append(tween);
            this.sequence.AppendInterval(this.waitingTime);
        }

        for(int i = this.positions.Count- 2; i >= 1; i--) {
        var tween = this.transform.DOMove(this.positions[i], this.movingTimes[i]);
        tween.SetEase(Ease.InOutQuint);
        this.sequence.Append(tween);
        this.sequence.AppendInterval(this.waitingTime);
        }
    }

IEnumerator Play() {
this.isPlaying = true;
this.CreateSequence();
this.sequence.Play();
yield return this.sequence.WaitForCompletion();
this.isPlaying = false;
}


    // Update is called once per frame
    void Update()
    {
        if(!this.isPlaying) {
        this.StartCoroutine(this.Play());
}
    }
}
