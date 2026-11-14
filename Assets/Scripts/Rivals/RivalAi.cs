using Unity.VisualScripting;
using UnityEngine;

public class RivalAi : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] Vector2 moveDirection;

    [Header("Raycast")]
    [SerializeField] Transform obstacleRaycastOrigin;
    [SerializeField] GameObject raycastForwardOrigin;
    [SerializeField] GameObject raycastBackwardOrigin;
    //Obstacles
    RaycastHit2D checkForward;
    RaycastHit2D checkLeft;
    RaycastHit2D checkRight;
    //Player
    RaycastHit2D checkPlayerForward;
    RaycastHit2D checkPlayerBackward;

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

        GameManager.instance.isInRivalRace = true;
        raycastBackwardOrigin.SetActive(false);
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(moveDirection * acceleration);
        if (rigidbody.linearVelocity.magnitude >= topSpeed)
        {
            rigidbody.linearVelocity = Vector2.ClampMagnitude(rigidbody.linearVelocity, topSpeed);
        }

        checkForward = Physics2D.Raycast(obstacleRaycastOrigin.position, Vector2.right, 20f, obstacleRayCastLayerMask);
        checkLeft = Physics2D.Raycast(transform.position, Vector2.up, 1.6f, obstacleRayCastLayerMask);
        checkRight = Physics2D.Raycast(transform.position, -Vector2.up, 1.6f, obstacleRayCastLayerMask);

        checkPlayerForward = Physics2D.Raycast(raycastForwardOrigin.transform.position, Vector2.up, 15f, playerRayCastLayerMask);
        checkPlayerForward = Physics2D.Raycast(raycastForwardOrigin.transform.position, -Vector2.up, 15f, playerRayCastLayerMask);

        checkPlayerBackward = Physics2D.Raycast(raycastBackwardOrigin.transform.position, Vector2.up, 15f, playerRayCastLayerMask);
        checkPlayerBackward = Physics2D.Raycast(raycastBackwardOrigin.transform.position, -Vector2.up, 15f, playerRayCastLayerMask);

        if (checkForward)
        {
            int random = Random.Range(0, 3);
            if(random == 0)
            {
                rigidbody.AddForce(Vector2.up * up * steeringSpeed);
            }
            if (random == 1)
            {
                rigidbody.AddForce(Vector2.up * down * steeringSpeed);
            }
            if(random == 2)
            {
                return;
            }

            Debug.Log(random);

            if (checkLeft)
            {
                rigidbody.AddForce(Vector2.up * down * steeringSpeed);
            }
            if (checkRight)
            {
                rigidbody.AddForce(Vector2.up * up * steeringSpeed);
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

        if(checkPlayerForward)
        {
            raycastBackwardOrigin.SetActive(true);
            raycastForwardOrigin.SetActive(false);
            //GameManager.instance.destroyRival();
            GameManager.instance.rivalryTimerOngoing = true;
        }
        if (checkPlayerBackward)
        {
            raycastForwardOrigin.SetActive(true);
            raycastBackwardOrigin.SetActive(false);
            GameManager.instance.rivalryTimerOngoing = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(obstacleRaycastOrigin.position, Vector2.right * 20);
        Gizmos.DrawRay(transform.position, Vector2.up * 1.5f);
        Gizmos.DrawRay(transform.position, -Vector2.up * 1.5f);

        Gizmos.color = Color.greenYellow;
        Gizmos.DrawRay(raycastForwardOrigin.transform.position, Vector2.up * 15);
        Gizmos.DrawRay(raycastForwardOrigin.transform.position, -Vector2.up * 15);
        Gizmos.DrawRay(raycastBackwardOrigin.transform.position, Vector2.up * 15);
        Gizmos.DrawRay(raycastBackwardOrigin.transform.position, -Vector2.up * 15);
    }
}
