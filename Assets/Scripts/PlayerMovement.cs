using System;
using TMPro;
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
    public float[] speedPerGears;

    [Header("Car Values")]
    public bool isAccellerating;
    public bool isBraking;
    public bool canSteer;
    public bool canUpShift;
    public int currentGear;
    public int currentSpeed;


    [Header("Player Components")]
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private PlayerInput playerInput;
    public Animator steeringWheel;
    public string[] animationStrings;

    [Header("Dashboard")]
    [SerializeField] private float speedDisplay;
    [SerializeField] private TextMeshProUGUI speedDisplayText;
    [SerializeField] private float gearDisplay;
    [SerializeField] private TextMeshProUGUI gearDisplayText;


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
        canUpShift = false;

        accellarationSpeed = gears[0];
        topSpeed = speedPerGears[0];

        currentGear = 0;
        currentSpeed = 0;

        steeringWheel.Play(animationStrings[0]);

        gearDisplay = 1f;
        speedDisplay = 0f;


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

        speedDisplay = rigidbody2D.linearVelocity.x;
        speedDisplayText.SetText(speedDisplay.ToString("F0"));

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
            previousGear();
        }
        else
        {
            rigidbody2D.linearDamping = 0.5f;
        }
        

        if (rigidbody2D.linearVelocity.magnitude >= topSpeed)
        {
            rigidbody2D.linearVelocity = Vector2.ClampMagnitude(rigidbody2D.linearVelocity, topSpeed);
            canUpShift = true;
            
            //nextGear();
        }

        if(rigidbody2D.linearVelocity.magnitude >= 0.01)
        {
            rigidbody2D.AddForce(Vector2.up * verticalInput * steering);
            if(verticalInput > 0.1f)
            {
                steeringWheel.Play(animationStrings[6]);
            }
            if (verticalInput < -0.1f)
            {
                steeringWheel.Play(animationStrings[4]);
            }
            if(verticalInput == 0f)
            {
                steeringWheel.Play(animationStrings[0]);
            }
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
        if(canUpShift == true)
        {
            nextGear();
            steeringWheel.Play(animationStrings[1]);
        }
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
            //Debug.Log($"currentGear switch {currentGear}");
            accellarationSpeed = 0;
            gearDisplay++;
            gearDisplayText.SetText(gearDisplay.ToString());
            for (int i = 0; i <= currentGear; i++)
            {
                //Debug.Log(i);
                accellarationSpeed += accellarationSpeed + gears[i];
                
            }
        }

        if(currentSpeed < speedPerGears.Length - 1)
        {
            currentSpeed += 1;
            //Debug.Log($"currentGear switch {currentSpeed}");
            topSpeed = 0;
            for (int i = 0; i <= currentSpeed; i++)
            {
                //Debug.Log(i);
                topSpeed = topSpeed + speedPerGears[i];
            }

            canUpShift = false;
        }

        //Debug.Log("Switched Gear");
        
    }

    void previousGear()
    {
        if(currentGear > 0)
        {
            gearDisplay--;
            gearDisplayText.SetText(gearDisplay.ToString());
            currentGear -= 1;
            //Debug.Log($"currentGear switch {currentGear}");
            accellarationSpeed = 0;
            for (int i = 0; i <= currentGear; i++)
            {
                Debug.Log(i);
                accellarationSpeed += accellarationSpeed + gears[i];
                steeringWheel.Play(animationStrings[2]);
            }
        }

        if (currentSpeed > 0)
        {
            currentSpeed -= 1;
            //Debug.Log($"currentGear switch {currentSpeed}");
            topSpeed = 0;
            for (int i = 0; i <= currentSpeed; i++)
            {
                Debug.Log(i);
                topSpeed += topSpeed + speedPerGears[i];
            }

            canUpShift = false;
        }
    }
}