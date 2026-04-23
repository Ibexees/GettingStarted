using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class sawHandler : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] float rotationSpeed = 400f;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioMixerGroup sfxMixerGroup;
    [SerializeField] private AudioClip idleSound;
    [SerializeField] private AudioClip cuttingSound;

    [Header("Particles")]
    [SerializeField] ParticleSystem cuttingParticles;

    private bool isCutting;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = sfxMixerGroup;
        audioSource.loop = true;
        audioSource.playOnAwake = true;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource.clip=idleSound;
        audioSource.Play();
        SetState(false);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            { SetState(true); }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        { SetState(false); }
    }

    private void SetState(bool cutting)
    {
        if (isCutting == cutting)
        { return; }

        if (cutting)
        {
            isCutting = true;
            audioSource.clip = cuttingSound;
        }
        else
        {
            isCutting = false;
            audioSource.clip = idleSound;
        }
        audioSource.Play();

    }
}
