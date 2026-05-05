using UnityEngine;

public class PlayerLocomotion : MonoBehaviour
{
    PlayerManager playerManager;
    AnimatorManager animatorManager;
    InputManager inputManager;

    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidbody;

    [Header("Abilities")]
    public bool hasDash = false;
    public float dashForce = 150f;
    public float dashCooldown = 2.0f;
    public float dashTime = 0f;

    [Header("Falling")]
    public float inAirTimer;
    public float leapingVelocity = 3f;
    public float fallingVelocity = 33f;
    public float rayCastHeightOffSet = 0.5f;
    public LayerMask groundLayer;

    [Header("Movement Flags")]
    public bool isSprinting;
    public bool isGrounded;
    public bool isJumping;

    [Header("Movement Speeds")]
    public float walkingSpeed = 1.5f;
    public float runningSpeed = 5f;
    public float sprintingSpeed = 7f;
    public float rotationSpeed = 15f;

    [Header("Jump Speed")] 
    public float jumpHeight = 3f;
    public float gravityIntensity = -8f;

    [Header("Sounds")]
    public AudioClip footSteps;
    [SerializeField] private float stepInterval = 0.5f;
    private float stepTimer;
    public AudioClip jumpSound;
    public AudioClip landingSound;

    private void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        animatorManager = GetComponent<AnimatorManager>();
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    public void HandleAllMovements()
    {
        HandleFallingAndLanding();

        if (playerManager.isInteracting)
            return;

        HandleMovement();
        HandleRotation();

    }

    private void HandleMovement()
    {
        //if (isJumping)
        //    return;
        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection = moveDirection + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        HandleFootsteps();

        if (isSprinting)
        {
            moveDirection = moveDirection * sprintingSpeed;
        }
        else
        {
            if (inputManager.moveAmount >= 0.5f)
            {
                moveDirection = moveDirection * runningSpeed;
            }
            else
            {
                moveDirection = moveDirection * walkingSpeed;
            }
        }


        //Vector3 movementVelocity = moveDirection;
        //playerRigidbody.linearVelocity = movementVelocity;



        //Vector3 velocity = playerRigidbody.linearVelocity;

        //velocity.x = moveDirection.x;
        //velocity.z = moveDirection.z;

        //playerRigidbody.linearVelocity = velocity;



        Vector3 velocity = playerRigidbody.linearVelocity;

        Vector3 target = moveDirection * (isGrounded ? 1f : 1f);

        velocity.x = target.x;
        velocity.z = target.z;

        playerRigidbody.linearVelocity = velocity;
    }

    public void HandleDash()
    {
        if (dashTime <= 0 && hasDash)
        {
            Debug.Log("Dash!");

            float Gravity = playerRigidbody.useGravity ? 1 : 0;
            playerRigidbody.useGravity = false;
            playerRigidbody.AddForce(transform.forward * dashForce * 100);
            playerRigidbody.useGravity = Gravity > 0;

            dashTime = dashCooldown;
        }
    }


    private void HandleRotation()
    {
        //if (isJumping)
        //    return;
        Vector3 targetDirection = Vector3.zero;

        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection = targetDirection + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;
        
        if (targetDirection == Vector3.zero)
        {
            targetDirection = transform.forward;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection * 0.2f);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.rotation = playerRotation;
    }


    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position;
        rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffSet;

        if (!isGrounded && !isJumping)
        {
            //if (!playerManager.isInteracting)
            //{
            //    animatorManager.PlayTargetAnimation("Falling", true);
            //}

            inAirTimer = inAirTimer + Time.deltaTime;
            playerRigidbody.AddForce(transform.forward * leapingVelocity);
            playerRigidbody.AddForce(-Vector3.up * fallingVelocity * inAirTimer);
        }

        if (Physics.SphereCast(rayCastOrigin, 0.2f, -Vector3.up, out hit, groundLayer))
        {
            if (!isGrounded && !playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Land", true);
            }

            inAirTimer = 0;
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        //RaycastHit hit;
        //Vector3 rayCastOrigin = transform.position;
        //rayCastOrigin.y = rayCastOrigin.y + rayCastHeightOffSet;

        //// 🔻 Если в воздухе
        //if (!isGrounded && !isJumping)
        //{
        //    if (!playerManager.isInteracting)
        //    {
        //        animatorManager.PlayTargetAnimation("Falling", true);
        //    }

        //    inAirTimer += Time.deltaTime;

        //    //  НАПРАВЛЕНИЕ ДВИЖЕНИЯ В ВОЗДУХЕ (вместо transform.forward)
        //    Vector3 airDirection = transform.forward * inputManager.verticalInput + transform.right * inputManager.horizontalInput;
        //    airDirection.Normalize();

        //    // движение в воздухе
        //    playerRigidbody.AddForce(airDirection * leapingVelocity);

        //    // падение вниз
        //    playerRigidbody.AddForce(-Vector3.up * fallingVelocity * inAirTimer);

        //    // Ограничение скорости в воздухе
        //    Vector3 velocity = playerRigidbody.linearVelocity;
        //    Vector3 horizontalVelocity = new Vector3(velocity.x, 0, velocity.z);

        //    float maxAirSpeed = 5f;

        //    if (horizontalVelocity.magnitude > maxAirSpeed)
        //    {
        //        Vector3 limitedVelocity = horizontalVelocity.normalized * maxAirSpeed;
        //        playerRigidbody.linearVelocity = new Vector3(limitedVelocity.x, velocity.y, limitedVelocity.z);
        //    }
        //}

        ////Проверка земли
        //if (Physics.SphereCast(rayCastOrigin, 0.2f, -Vector3.up, out hit, groundLayer))
        //{
        //    if (!isGrounded && !playerManager.isInteracting)
        //    {
        //        animatorManager.PlayTargetAnimation("Land", true);
        //    }

        //    inAirTimer = 0;
        //    isGrounded = true;
        //}
        //else
        //{
        //    isGrounded = false;
        //}
    }

    public void HandleJumping()
    {
        if (isGrounded)
        {
            animatorManager.animator.SetBool("isJumping", true);
            animatorManager.PlayTargetAnimation("Jump", false);

            AudioClip jumpAudio = jumpSound;
            AudioSource.PlayClipAtPoint(jumpAudio, transform.position);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity;
            playerRigidbody.linearVelocity = playerVelocity;
        }
    }



    private void HandleFootsteps()
    {
        // не двигается — не играем звук
        if (inputManager.moveAmount < 0.1f || !isGrounded)
            return;

        stepTimer -= Time.deltaTime;

        if (stepTimer <= 0f)
        {
            AudioSource.PlayClipAtPoint(footSteps, transform.position);

            // скорость шагов зависит от бега
            stepTimer = isSprinting ? stepInterval * 0.6f : stepInterval;
        }
    }

}
