using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class leverFunction : MonoBehaviour
{

private bool leverOn = false;
private InputAction interactAction;

    [SerializeField]
    private Transform onPosition;

    [SerializeField]
    private UnityEngine.Transform offPosition;

    private Transform target;

    [SerializeField]
    private GameObject leverHandle;

    [SerializeField]
    private SphereCollider leverRange;

    private bool leverInRange = false;

    movingPlatform platform4;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        platform4 = GameObject.Find("HorizontalPlatform4").GetComponent<movingPlatform>();
        this.interactAction = InputSystem.actions.FindAction("Interact");
        this.interactAction.Enable();
    }

    public void setLever(bool state)
    { 
        leverOn = state;
        platform4.enabled = state;
        setLeverTarget();
        
    }

    void ToggleLever()
    {
        this.leverOn = !this.leverOn;
        platform4.enabled = leverOn;
        setLeverTarget();
    }

    void setLeverTarget()
    {
        if (leverOn)
        {
            target = onPosition.transform;
        }
        else
        {
            target = offPosition.transform;
        }
    }

  

    // Update is called once per frame
    void Update()
    {
        if (target == null) return;

       leverHandle.transform.position = Vector3.Lerp(
       leverHandle.transform.position,
       target.position,
       Time.deltaTime * 5f
   );

        leverHandle.transform.rotation = Quaternion.Lerp(
        leverHandle.transform.rotation,
        target.rotation,
        Time.deltaTime * 5f
        );
    }

    private void FixedUpdate()
    {
        if (this.interactAction.WasPressedThisFrame() && leverInRange)
        {
            UnityEngine.Debug.Log("E pressed!");
            this.ToggleLever();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("inLeverRange");
        leverInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("outLeverRange");
        leverInRange = false;
    }
}
