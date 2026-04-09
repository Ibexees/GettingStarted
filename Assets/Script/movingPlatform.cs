using UnityEngine;

public class movingPlatform : MonoBehaviour
{

    [SerializeField]
    Transform targetTransform;

    private Vector3 initialPosition;

    [SerializeField]
    private float platformSpeed;

    private void Start()
    {
        initialPosition = transform.position;
    }

    void FixedUpdate()
    {
        float pingPong = Mathf.PingPong(Time.fixedTime * platformSpeed, 1.0f);

        Vector3 newPosition = Vector3.Lerp(initialPosition, targetTransform.position, pingPong);
        transform.position = newPosition;
    }
}
