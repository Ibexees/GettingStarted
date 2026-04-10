using UnityEngine;

public class deathPlane : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    Transform respawnTransform;

    [SerializeField]
    Rigidbody character;

    leverFunction lever;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lever = GameObject.Find("lever").GetComponent<leverFunction>();
        lever.setLever(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTrigger");

        Rigidbody rb = other.GetComponent<Rigidbody>();

        CharacterController controller = other.GetComponent<CharacterController>();

        if (controller != null)
        {
            // CharacterController deaktivieren
            controller.enabled = false;

            // Position setzen
            other.transform.position = respawnTransform.position;

            // CharacterController wieder aktivieren
            controller.enabled = true;
        
    }
        lever.setLever(false);


    }

    // Update is called once per frame

}
