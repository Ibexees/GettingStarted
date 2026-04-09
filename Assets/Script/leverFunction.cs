using UnityEngine;
using System.Runtime.CompilerServices;
using UnityEngine.InputSystem;
using System;

public class leverFunction : MonoBehaviour
{

private bool leverOn = false;
private InputAction interactAction;

    [SerializeField]
    private Transform onPosition;

    [SerializeField]
    private UnityEngine.Transform offPosition;

    [SerializeField]
    private GameObject leverHandle;

    [SerializeField]
    private SphereCollider leverRange;

    private bool leverInRange = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.interactAction = InputSystem.actions.FindAction("Interact");
        this.interactAction.Enable();
    }

void ToggleLever()
    {
        this.leverOn = !this.leverOn;
        if(this.leverOn)
        {
            this.leverHandle.transform.SetPositionAndRotation(this.offPosition.position, this.offPosition.rotation);
        }
        else
        {
            this.leverHandle.transform.SetPositionAndRotation(this.onPosition.position, this.onPosition.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
      
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
