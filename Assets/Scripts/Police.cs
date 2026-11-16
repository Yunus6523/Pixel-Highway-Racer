using UnityEngine;
using UnityEngine.EventSystems;

public class Police : MonoBehaviour
{
    [Header("Cop Car Component")]
    public float supposedSpeed;
    public float speed;
    public float steeringSpeed;
    public float breakingPower;


    [Header("Movement Component")]
    public Transform target;
    private Vector2 moveDirection;
    private Vector2 up;
    private Vector2 down;
    public float distance;

    [SerializeField] private Rigidbody2D rigidbody;

    public ChaseManager chaseManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameManager.instance.spawnedCar.transform;
        
        rigidbody = GetComponent<Rigidbody2D>();

        moveDirection = new Vector2 (1f, 0f);

        int random = Random.Range(0, 3);
        switch (random)
        {
            case 0:
                speed = 40f;
                break;
             case 1:
                speed = 30f;
                break;
            case 2:
                speed = 50f;
                break;
        }
        speed = supposedSpeed;

        up = new Vector2 (0f, 2f);
        down = new Vector2 (0f, -2f);
        
    }

    // Update is called once per frame
    void Update()
    {
        speed = supposedSpeed;

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        distance = Vector2.Distance(transform.position, target.position);

        if (distance < 1)
        {
            speed = rigidbody.linearDamping + breakingPower;
        }
        else if (distance > 1)
        {
            speed = supposedSpeed;
        }

        RaycastHit2D checkForward = Physics2D.Raycast(transform.position, Vector2.right);
        checkForward.distance = 10f;
        RaycastHit2D checkLeft = Physics2D.Raycast(transform.position, Vector2.up);
        checkLeft.distance = 2f;
        RaycastHit2D checkRight = Physics2D.Raycast(transform.position, -Vector2.up);
        checkRight.distance = 2f;

        Debug.DrawRay(transform.position, moveDirection, Color.green);

        if (checkForward)
        {
            speed = rigidbody.linearDamping + breakingPower;
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                rigidbody.AddForce(Vector2.up * up * steeringSpeed);
            }
            if (random == 1)
            {
                rigidbody.AddForce(Vector2.up * down * steeringSpeed);
            }
            if (checkLeft)
            {
                rigidbody.AddForce(Vector2.up * down * steeringSpeed);
            }
            if (checkRight)
            {
                rigidbody.AddForce(Vector2.up * up * steeringSpeed);
            }
        }
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(moveDirection * speed);

        if (rigidbody.linearVelocity.magnitude >= speed)
        {
            rigidbody.linearVelocity = Vector2.ClampMagnitude(rigidbody.linearVelocity, speed);
        }
    }
}
