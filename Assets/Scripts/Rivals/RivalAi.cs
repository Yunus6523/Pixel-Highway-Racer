using Unity.VisualScripting;
using UnityEngine;

public class RivalAi : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] Vector2 moveDirection;

    [Header("Raycast")]
    [SerializeField] Transform raycastForwardOrigin;
    [SerializeField] Transform raycastBackwardOrigin;
    //Obstacles
    RaycastHit2D checkForward;
    RaycastHit2D checkLeft;
    RaycastHit2D checkRight;
    //Player

    [Header("Layer Mask")]
    [SerializeField] LayerMask obstacleRayCastLayerMask;
    [SerializeField] LayerMask playerRayCastLayerMask;


    [Header("Steering")]
    [SerializeField] Vector2 up;
    [SerializeField] Vector2 down;
    [SerializeField] float steeringSpeed;


    [Header("Speed")]
    [SerializeField] float acceleration = 20f;


    [Header("Top Speed")]
    [SerializeField] float topSpeed = 100f;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        moveDirection = new Vector2 (1f, 0);
        up = new Vector2 (0f, 1f);
        down = new Vector2(0f, -1f);
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(moveDirection * acceleration);
        if (rigidbody.linearVelocity.magnitude >= topSpeed)
        {
            rigidbody.linearVelocity = Vector2.ClampMagnitude(rigidbody.linearVelocity, topSpeed);
        }

        checkForward = Physics2D.Raycast(raycastForwardOrigin.position, Vector2.right, 20f, obstacleRayCastLayerMask);
        checkLeft = Physics2D.Raycast(transform.position, Vector2.up, 2f, obstacleRayCastLayerMask);
        checkRight = Physics2D.Raycast(transform.position, -Vector2.up, 2f, obstacleRayCastLayerMask);

        if (checkForward)
        {
            int random = Random.Range(0, 2);
            if(random == 0)
            {
                rigidbody.linearDamping = 10f;
            }
            if(random == 1)
            {
                int randomSteering = Random.Range(0, 2);
                if (randomSteering == 0)
                {
                    rigidbody.AddForce(Vector2.up * up * steeringSpeed);
                }
                if (randomSteering == 1)
                {
                    rigidbody.AddForce(Vector2.up * down * steeringSpeed);
                   
                }
            }
            if (checkLeft)
            {
                rigidbody.AddForce(Vector2.up * down * steeringSpeed);
            }
            if (checkRight)
            {
                rigidbody.AddForce(Vector2.up * up * steeringSpeed);
            }
            else if(random == 0||rigidbody.linearVelocity.x > 20)
            {
                rigidbody.linearDamping = 0;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(raycastForwardOrigin.position, Vector2.right * 20);
        Gizmos.DrawRay(transform.position, Vector2.up * 2f);
        Gizmos.DrawRay(transform.position, -Vector2.up * 2);

        Gizmos.color = Color.greenYellow;
        Gizmos.DrawRay(raycastForwardOrigin.position, Vector2.up * 15);
        Gizmos.DrawRay(raycastForwardOrigin.position, -Vector2.up * 15);
        Gizmos.DrawRay(raycastBackwardOrigin.position, Vector2.up * 15);
        Gizmos.DrawRay(raycastBackwardOrigin.position, -Vector2.up * 15);
    }
}
