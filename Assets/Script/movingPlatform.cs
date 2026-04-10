using UnityEngine;

public class movingPlatform : MonoBehaviour
{

    [SerializeField]
    Transform targetTransform;

    private Vector3 initialPosition;

    [SerializeField]
    private float platformSpeed;

   

    private Vector3 lastPosition;
    Vector3 newPosition;

    Vector3 velocity;

    private void Start()
    {
        initialPosition = transform.position;
        lastPosition = transform.position;
    }

    void FixedUpdate()
    {
        float platformFunction = Mathf.Sin(Time.time * platformSpeed) * 0.5f + 0.5f;//Mathf.PingPong(Time.fixedTime * platformSpeed, 1.0f);

        lastPosition = newPosition;
        newPosition = Vector3.Lerp(initialPosition, targetTransform.position, platformFunction);
        velocity = (newPosition - lastPosition) / Time.fixedDeltaTime;
        transform.position = newPosition;
    }

    public Vector3 GetVelocity()
    { 
        return velocity;
    }
}
