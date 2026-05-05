using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Animator animator;
    InputManager inputManager;
    PlayerLocomotion playerLocomotion;

    CameraManager cameraManager;

    public bool isInteracting;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        playerLocomotion = GetComponent<PlayerLocomotion>();

        cameraManager = FindAnyObjectByType<CameraManager>();
    }


    private void Update()
    {
        inputManager.HandleAllInputs();

    }

    private void FixedUpdate()
    {
        playerLocomotion.HandleAllMovements();
        //Debug.Log(playerLocomotion.dashTime);
        if (playerLocomotion.dashTime > 0) {
            playerLocomotion.dashTime -= Time.deltaTime;
        }    
    }

    private void LateUpdate()
    {
            cameraManager.HandleAllCameraMovement();

        isInteracting = animator.GetBool("isInteracting");
        playerLocomotion.isJumping = animator.GetBool("isJumping");
        animator.SetBool("isGrounded", playerLocomotion.isGrounded);
    }

}
