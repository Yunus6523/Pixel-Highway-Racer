using Unity.VisualScripting;
using UnityEngine;

public class NPCCars : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D rigidbody;
    [SerializeField] Vector2 moveDirection;

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
