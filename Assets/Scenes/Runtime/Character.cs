using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.InputSystem;
public class Character : MonoBehaviour
{
    private bool isJumping = false;
    private float jumpCooldownTimer;
    private CharacterController controller;
    private InputAction moveAction;
    private InputAction jumpAction;
    [SerializeField]
    private float jumpCooldown;
    //We set gravity lower than in real live as it is more fun!
    [SerializeField]
    private float gravity;
    [SerializeField]
    private float characterSpeed;

    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float dampening;
    [SerializeField]
    private Transform cameraTransform;
    private Vector3 characterMovement;
    private Vector3 combinedMovement;
    private Vector3 jumpVelocity;
    private Vector3 characterGravity;
    void Start()
    {
        this.controller = this.GetComponent<CharacterController>();
        this.animator = this.GetComponent<Animator>();
        this.moveAction = InputSystem.actions.FindAction("Move");
        this.jumpAction = InputSystem.actions.FindAction("Jump");
        this.jumpCooldownTimer = 0.0f;
    }

    private Animator animator;
    private void  SetAnimationState(Vector2 inputMovement)
    {
        this.animator.SetFloat("RunningSpeed", inputMovement.x);
    }

    void HandleJumping()
    {
        if (this.controller.isGrounded && this.isJumping && this.jumpCooldownTimer <= 0.0f)
        {
            this.jumpVelocity = Vector3.zero;
            this.isJumping = false;
        }
        if (!this.isJumping && this.jumpAction.WasPressedThisFrame())
        {
            this.characterGravity = Vector3.zero;
            this.jumpVelocity = Vector3.zero;
            this.jumpVelocity.y = this.jumpSpeed;
            this.jumpCooldownTimer = this.jumpCooldown;
            this.isJumping = true;
        }
        if (this.jumpVelocity.y > 0.0f)
        {
            this.jumpVelocity.y -= Time.deltaTime;
        }
        else
        {
            this.jumpVelocity = Vector3.zero;
        }
        this.jumpCooldownTimer -= Time.deltaTime;
    }

    Vector3 GetPlatformVelocity()
    {
        RaycastHit hit;

        // Raycast nach unten
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f))
        {
            
            // Layer check
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Platform"))
            {
                
                movingPlatform platform = hit.collider.GetComponent<movingPlatform>();
                Debug.Log("plat:" +platform);

                if (platform != null)
                {
                    Debug.Log("Platform found");
                    return platform.GetVelocity();
                }
            }
        }

        return Vector3.zero;
    }



    void Update()
    {
        
        this.HandleJumping();
        var inputMovement = this.moveAction.ReadValue<Vector2>();
        var inputRightDirection = this.cameraTransform.right;
        var inputForwardDirection = this.cameraTransform.forward;
        inputRightDirection.y = 0.0f;
        inputForwardDirection.y = 0.0f;
        inputRightDirection.Normalize();
        inputForwardDirection.Normalize();

        this.SetAnimationState(inputMovement);
        //Since we do not use the physics system, we have to simulate gravity ourselves
        if (this.controller.isGrounded)
        {
            this.characterGravity.y = 0.0f;
        }
        this.characterGravity.y += this.gravity * Time.deltaTime;
        this.characterMovement += this.characterGravity * Time.deltaTime;
        this.characterMovement += this.jumpVelocity * Time.deltaTime;

        this.characterMovement += inputRightDirection * inputMovement.x * this.characterSpeed * Time.deltaTime;
        this.characterMovement += inputForwardDirection * inputMovement.y * this.characterSpeed * Time.deltaTime;
        this.characterMovement *= (1 - this.dampening);
        Vector3 characterForward = this.characterMovement;
        characterForward.y = 0.0f;
        if (characterForward.sqrMagnitude > 0.0001f)
        {
            this.transform.forward = characterForward.normalized;
        }

        setCombinedMovement();
        
        this.controller.Move(this.combinedMovement);
    }

    private void setCombinedMovement()
    {
        if (controller.isGrounded)
        {
            Vector3 velocity = new Vector3(GetPlatformVelocity().x, 0, GetPlatformVelocity().z);
            combinedMovement = characterMovement + velocity * Time.deltaTime;
        }
        else
        {
            combinedMovement = characterMovement;
        }
        
    }
}
