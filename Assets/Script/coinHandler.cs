using UnityEngine;
using UnityEngine.Audio;

public class coinHandler : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private AudioClip collectSound;
    [SerializeField] private AudioMixerGroup sfxMixerGroup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        { return; }

        var collectSoundGameObject = new GameObject("CollectSound");
        collectSoundGameObject.transform.position = transform.position;
        var audioSource = collectSoundGameObject.AddComponent<AudioSource>();
        audioSource.clip = collectSound;
        audioSource.outputAudioMixerGroup = sfxMixerGroup;
        audioSource.Play();
        Destroy(collectSoundGameObject, collectSound.length);

        Destroy(this.gameObject);
        
    }
}
