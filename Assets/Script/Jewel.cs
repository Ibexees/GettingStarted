using System.Collections;
using UnityEngine;

public class Jewel : MonoBehaviour
{
    [SerializeField] GameObject jewel;
    [SerializeField] private Transform targetTransform;
    private Transform initialTransform;
    private float duration = 10.0f;
    
    private int defeatCount;

    public void SpawnJewel()
    { 
        defeatCount++;
        if (defeatCount == 3)
        {
            UIManager.Instance.unlock();
            this.gameObject.SetActive(true);
            StartCoroutine(SpawnAnimation(duration));
        }
        else
        { return; }
    
    }

    IEnumerator SpawnAnimation(float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime /100;
            float percent = Mathf.Clamp01(elapsed / duration);

            this.transform.position = Vector3.Lerp(initialTransform.position, targetTransform.position, elapsed/duration);
            yield return null;
        }

        transform.position = targetTransform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {

            PlayerCharacter character = other.GetComponent<PlayerCharacter>();

            character.enabled = false;
            UIManager.Instance.Victory();
        }
        Destroy(this.gameObject);


    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialTransform = this.transform;
        this.gameObject.SetActive(false);
    }


}
