using UnityEngine;
using UnityEngine.EventSystems;

public class Police : MonoBehaviour
{
    public float speed;
    public Transform target;
    private Vector2 moveDirection;
    public Vector3 distance;

    [SerializeField] private Rigidbody2D rigidbody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameManager.instance.spawnedCar.transform;
        
        rigidbody = GetComponent<Rigidbody2D>();

        moveDirection = new Vector2 (1f, 0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        distance = transform.position - target.position;
        if(distance.x <= -1)
        {
            Debug.Log("arrested suspect");
            speed = 0;
        }

        if (rigidbody.linearVelocity.magnitude >= speed)
        {
            rigidbody.linearVelocity = Vector2.ClampMagnitude(rigidbody.linearVelocity, speed);
        }
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(moveDirection * speed);
    }
}
