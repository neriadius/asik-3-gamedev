using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    PlayerControls playerControls;
    AnimatorManager animatorManager;

    public Vector2 moveInput;
    private float moveAmount;

    public float verticalInput;
    public float horizontalInput;


    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
    }


    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => moveInput = i.ReadValue<Vector2>();
        }
        playerControls.Enable();
    }

    private void OnDisable()
        {
            playerControls.Disable();
    }


    public void HandleAllInputs()
    {
        HandleMovementInput();

        //handle jump and action inputs
    }

    private void HandleMovementInput()
    {
        verticalInput = moveInput.y;
        horizontalInput = moveInput.x;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput));
        animatorManager.UpdateAnimatorValues(0, moveAmount);
    }

}
