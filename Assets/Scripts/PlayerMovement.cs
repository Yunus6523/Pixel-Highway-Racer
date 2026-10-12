using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [Header("Car Stock Values")]
    public float topSpeed;
    public float accellarationSpeed;
    public float brakePower;
    public float steering;
    public float[] gears;

    [Header("Car Values")]
    public bool isAccellerating;
    public bool isBraking;
    public bool canSteer;
    public int currentGear;


    [Header("Player Components")]
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private PlayerInput playerInput;


    [Header("Direction")]
    [SerializeField] private Vector2 moveDirection;


    [Header("Player Input")]
    [SerializeField] private InputAction throttleAction;
    [SerializeField] private InputAction brakeAction;
    [SerializeField] private InputAction steerAction;
    [SerializeField] private InputAction shiftUpAction;
    [SerializeField] private InputAction shiftDownAction;

    void Start()
    {
        //Initialize Cars Stock Values
        isAccellerating = false;
        isBraking = false;
        canSteer = false;
        accellarationSpeed = gears[0];
        currentGear = 0;

        //Initialize Core Components
        rigidbody2D = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();

        //Define Direction
        moveDirection = new Vector2(1f, 0f);

        //Define Input
        throttleAction = playerInput.actions.FindAction("Throttle");
        brakeAction = playerInput.actions.FindAction("Brake");
        steerAction = playerInput.actions.FindAction("Steer");
        shiftUpAction = playerInput.actions.FindAction("Shift Up");
        shiftDownAction = playerInput.actions.FindAction("Shift Down");

        //Initialize Input
        throttleAction.performed += throttle;
        throttleAction.canceled += slowDown;
        brakeAction.performed += brake;
        brakeAction.canceled += releaseBrake;
        shiftUpAction.performed += shiftNextGear;
        shiftDownAction.performed += shiftPreviousGear;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(rigidbody2D.linearVelocity.x);
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
            rigidbody2D.linearVelocity = Vector2.ClampMagnitude(rigidbody2D.linearVelocity, topSpeed);
            //nextGear();
        }

        if(rigidbody2D.linearVelocity.magnitude >= 0.01)
        {
            rigidbody2D.AddForce(Vector2.up * verticalInput * steering);
        }
    }

    void throttle(InputAction.CallbackContext context)
    {
        isAccellerating = true;
    }

    void slowDown(InputAction.CallbackContext context)
    {
        isAccellerating = false;
    }

    void brake(InputAction.CallbackContext context)
    {
        isBraking = true;
    }

    void releaseBrake(InputAction.CallbackContext context)
    {
        isBraking = false;
    }

    void shiftNextGear(InputAction.CallbackContext context)
    {
        nextGear();
    }

    void shiftPreviousGear(InputAction.CallbackContext context)
    {
        previousGear();
    }

    void nextGear()
    {
        if(currentGear < gears.Length-1)
        {
            currentGear += 1;
            Debug.Log($"currentGear switch {currentGear}");
            accellarationSpeed = 0;
            for (int i = 0; i <= currentGear; i++)
            {
                Debug.Log(i);
                accellarationSpeed += accellarationSpeed + gears[i];
            }
        }
        
        Debug.Log("Switched Gear");
        
    }

    void previousGear()
    {
        if(currentGear > 0)
        {
            currentGear -= 1;
            Debug.Log($"currentGear switch {currentGear}");
            accellarationSpeed = 0;
            for (int i = 0; i <= currentGear; i++)
            {
                Debug.Log(i);
                accellarationSpeed += accellarationSpeed + gears[i];
            }
        }
    }
}