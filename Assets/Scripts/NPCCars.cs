using Unity.VisualScripting;
using UnityEngine;

public class NPCCars : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] Vector2 moveDirection;

    [Header("Raycast")]
    [SerializeField] Vector2 rayCastOrigin;

    [Header("Steering")]
    [SerializeField] Vector2 up;
    [SerializeField] Vector2 down;
    [SerializeField] float steeringSpeed;

    [Header("Speed")]
    [SerializeField] float twentyKMH = 5f;
    [SerializeField] float thirtyKMH = 10f;
    [SerializeField] float fiftyKMH = 15f;
    [SerializeField] float hundredKMH = 20f;

    [Header("Top Speed")]
    [SerializeField] float twenty = 20f;
    [SerializeField] float thirty = 30f;
    [SerializeField] float fifty = 50f;
    [SerializeField] float hundred = 100f;

    [Header("Speed Index")]
    [SerializeField] int randomizedIndex;

    [Header("Player")]
    [SerializeField] GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        moveDirection = new Vector2(1f, 0f);

        randomizedIndex = Random.Range(0, 5);

        up = new Vector2(0f, 1f);
        down = new Vector2(0f, -1f);

        Debug.DrawRay(rayCastOrigin, moveDirection, Color.green);
    }

    // Update is called once per frame
    void Update()
    {
        player = GameManager.instance.spawnedCar;
        var distance = Vector2.Distance(transform.position, player.transform.position);

        Debug.Log(distance);

        if(distance >= 100)
        {
            Destroy(this.gameObject);
        }

        //Direction
        RaycastHit2D checkForward = Physics2D.Raycast(transform.position, Vector2.right);
        checkForward.distance = 5f;
        RaycastHit2D checkLeft = Physics2D.Raycast(transform.position, Vector2.up);
        checkLeft.distance = 2f;
        RaycastHit2D checkRight = Physics2D.Raycast(transform.position, -Vector2.up);
        checkRight.distance = 2f;

        

        if (checkForward)
        {
            int random = Random.Range(0, 2);
            if (random == 0)
            {
                rigidbody.linearDamping = 5f;
                Debug.Log("BREAKING!");
            }
            else
            {
                rigidbody.linearDamping = 0f;
            }
        }
    }

    private void FixedUpdate()
    {
        switch (randomizedIndex)
        {
            case 0:
                rigidbody.AddForce(moveDirection * twentyKMH);
                if(rigidbody.linearVelocity.magnitude >= twenty)
                {
                    rigidbody.linearVelocity = Vector2.ClampMagnitude(rigidbody.linearVelocity, twenty);
                }
                Debug.Log("Driving Twenty");
                break;
            case 1:
                rigidbody.AddForce(moveDirection * thirtyKMH);
                if (rigidbody.linearVelocity.magnitude >= thirty)
                {
                    rigidbody.linearVelocity = Vector2.ClampMagnitude(rigidbody.linearVelocity, thirty);
                }
                Debug.Log("Driving Thirty");
                break;
            case 2:
                rigidbody.AddForce(moveDirection * fiftyKMH);
                if (rigidbody.linearVelocity.magnitude >= fifty)
                {
                    rigidbody.linearVelocity = Vector2.ClampMagnitude(rigidbody.linearVelocity, fifty);
                }
                Debug.Log("Driving Fifty");
                break;
            case 4:
                rigidbody.AddForce(moveDirection * hundredKMH);
                if (rigidbody.linearVelocity.magnitude >= hundred)
                {
                    rigidbody.linearVelocity = Vector2.ClampMagnitude(rigidbody.linearVelocity, hundred);
                }
                Debug.Log("Driving Hundred");
                break;
        }
    }
}
