using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public InputActionAsset inputActions;

    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction lookAction;

    private Vector2 moveAmt;
    private Vector2 lookAmt;

    private Animator animator;
    private Rigidbody rb;

    public float moveSpeed = 5f;
    public float jumpSpeed = 5f;
    public float lookSpeed = 15f;


    private void Awake()
    {
        moveAction = inputActions.FindAction("Move");
        jumpAction = inputActions.FindAction("Jump");
        lookAction = inputActions.FindAction("Look");

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveAmt = moveAction.ReadValue<Vector2>();
        lookAmt = lookAction.ReadValue<Vector2>();

        if (jumpAction.WasPressedThisFrame())
        {
            Jump();
        }
    }


    private void FixedUpdate()
    {
        Walking();
        Rotating();
    }


    private void OnEnable()
    {
        inputActions.FindActionMap("Player").Enable();
    }


    private void OnDisable()
    {
        inputActions.FindActionMap("Player").Disable();

    }



    public void Jump()
    {
        rb.AddForceAtPosition(new Vector3(0, jumpSpeed, 0), Vector3.up, ForceMode.Impulse);
        animator.SetTrigger("Jump");
        //animator.SetFloat("Speed", 0);
    }


    private void Walking()
    {
        Vector3 moveDir = transform.forward * moveAmt.y + transform.right * moveAmt.x;

        float speed = moveAmt.magnitude;

        animator.SetFloat("Speed", speed);

        rb.MovePosition(transform.position + moveDir * moveSpeed * Time.deltaTime);
    }


    private void Rotating()
    {

        if (moveAmt.y != 0)
        {
        float rotationAmount = lookAmt.x * lookSpeed * Time.deltaTime;

        Quaternion deltaRotation = Quaternion.Euler(0, rotationAmount, 0);
        rb.MoveRotation(rb.rotation * deltaRotation);
        }
    }

}