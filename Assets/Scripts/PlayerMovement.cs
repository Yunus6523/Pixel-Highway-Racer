using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("Car Stock Values")]
    public float topSpeed;
    public float accellarationSpeed;
    //public float currentSpeed;
    public float brakePower;
    public float steering;

    [Header("Car Values")]
    public bool isAccellerating;
    public bool isBraking;
    public bool canSteer;

    [Header("Player Components")]
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private PlayerInput playerInput;

    [Header("Direction")]
    [SerializeField] private Vector2 moveDirection;

    [Header("Player Input")]
    [SerializeField] private InputAction throttleAction;
    [SerializeField] private InputAction brakeAction;
    [SerializeField] private InputAction steerAction;

    void Start()
    {
        //Initialize Cars Stock Values
        isAccellerating = false;
        isBraking = false;
        canSteer = false;

        //Initialize Core Components
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        //Define Direction
        moveDirection = new Vector2(1f, 0f);

        //Define Input
        throttleAction = playerInput.actions.FindAction("Throttle");
        brakeAction = playerInput.actions.FindAction("Brake");
        steerAction = playerInput.actions.FindAction("Steer");

        //Initialize Input
        throttleAction.performed += throttle;
        throttleAction.canceled += slowDown;
        brakeAction.performed += brake;
        brakeAction.canceled += releaseBrake;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(rigidbody2D.linearVelocity.x);
    }

    void FixedUpdate()
    {
        Vector2 input = steerAction.ReadValue<Vector2>();
        float horizontalInput = input.x;
        float verticalInput = input.y;
        

        if (isAccellerating == true)
        {
            rigidbody2D.AddForce(moveDirection * accellarationSpeed);
            rigidbody2D.linearDamping = 0.1f;
        }
        else
        {
            rigidbody2D.linearDamping = 0.5f;
        }

        if (isBraking == true)
        {
            //rigidbody2D.AddForce(-moveDirection / brakePower);
            rigidbody2D.linearDamping = brakePower;
        }
        else
        {
            rigidbody2D.linearDamping = 0.5f;
        }
        

        if (rigidbody2D.linearVelocity.magnitude >= topSpeed)
        {
            //rigidbody2D.linearVelocity = Vector2.ClampMagnitude(rigidbody2D.linearVelocity, topSpeed);
            rigidbody2D.linearVelocity = rigidbody2D.linearVelocity * topSpeed;
        }

        if(rigidbody2D.linearVelocity.magnitude >= 0.01)
        {
            rigidbody2D.AddForce(Vector2.up * verticalInput * steering);
        }
    }

    void throttle(InputAction.CallbackContext context)
    {
        isAccellerating = true;
        Debug.Log("Vroom Vroom");
    }

    void slowDown(InputAction.CallbackContext context)
    {
        isAccellerating = false;
    }

    void brake(InputAction.CallbackContext context)
    {
        Debug.Log("SKREEEEE!");
        isBraking = true;
    }

    void releaseBrake(InputAction.CallbackContext context)
    {
        isBraking = false;
    }
}