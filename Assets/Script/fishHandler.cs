using DG;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.TextCore.Text;

public class fishHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private List<Vector3> positions;
    [SerializeField] private List<float> movingTimes;
    [SerializeField] private float waitingTime;
    [SerializeField] private int initialRotation;
    private bool isPlaying = false;
    private Sequence sequence;
    private Vector3 lastPosition;
    private bool squished;
    private AudioSource squishAudioSource;
    
    

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

        // ---------- VORWÄRTS ----------
        for (int i = 0; i < positions.Count - 1; i++)
        {
            Vector3 from = positions[i];
            Vector3 to = positions[i + 1];

            Vector3 dir = (to - from).normalized;

            if (dir != Vector3.zero)
            {
                Quaternion rot = Quaternion.LookRotation(dir) * Quaternion.Euler(0, initialRotation, 0);

                this.sequence.AppendCallback(() =>
                {
                    transform.DORotateQuaternion(rot, 0.3f);
                });
            }

            this.sequence.Append(
                transform.DOMove(to, movingTimes[i])
                    .SetEase(Ease.InOutQuint)
            );

            this.sequence.AppendInterval(waitingTime);
        }

        // ---------- RÜCKWÄRTS ----------
        for (int i = positions.Count - 2; i >= 0; i--)
        {
            Vector3 from = positions[i + 1];
            Vector3 to = positions[i];

            Vector3 dir = (to - from).normalized;

            if (dir != Vector3.zero)
            {
                Quaternion rot = Quaternion.LookRotation(dir) * Quaternion.Euler(0, initialRotation, 0);

                this.sequence.AppendCallback(() =>
                {
                    transform.DORotateQuaternion(rot, 0.3f);
                });
            }

            this.sequence.Append(
                transform.DOMove(to, movingTimes[i]) // ✅ KEIN i-1!
                    .SetEase(Ease.InOutQuint)
            );

            this.sequence.AppendInterval(waitingTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        PlayerCharacter character = other.GetComponentInParent<PlayerCharacter>();


        if (character == null)
        {
            Debug.Log("Character not found on: " + other.name);
            return;
        }

        // Prüfen ob Spieler über dem Gegner ist
        if (character.transform.position.y > transform.position.y + 0.1f)
        {
             character.Bounce(2f); // Stärke anpassen
            squishAudioSource.Play();

        
                transform.DOScale(new Vector3(0.12f, 0.02f, 0.12f), 0.15f)
                .OnComplete(() =>
                {
                    transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f), 2f);
                });
            }
    }

    private void Start()
    {
        squishAudioSource = this.GetComponent<AudioSource>();
    }

    IEnumerator Play()
    {
        this.isPlaying = true;
        this.CreateSequence();
        this.sequence.Play();
        yield return this.sequence.WaitForCompletion();
        this.isPlaying = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (!this.isPlaying)
        {
            this.StartCoroutine(this.Play());
        }
    }
}
