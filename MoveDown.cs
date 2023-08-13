using UnityEngine;

public class MoveDown : MonoBehaviour
{
    private readonly float speed = 2500.0f;
    private readonly float zDestroy = -8.0f;
    private readonly float rotationRange = 100f;
    private Rigidbody objectRb;
    private GameManager gameManager;
    private readonly float speedDivergence = 100f;
    private CollisionDetector collisionDetector;

    // Start is called before the first frame update
    void Start()
    {
        collisionDetector = gameObject.GetComponent<CollisionDetector>();

        float xSpeed = Random.Range(-speedDivergence, speedDivergence);
        float xRotation = Random.Range(-rotationRange, rotationRange);
        float yRotation = Random.Range(-rotationRange, rotationRange);
        float zRotation = Random.Range(-rotationRange, rotationRange);
        Debug.Log(xRotation);
        float speedMultiplier = Random.Range(-speedDivergence, speedDivergence);

        Vector3 direction = new(xSpeed, 0, -(speed + speedMultiplier));

        objectRb = GetComponent<Rigidbody>();
        objectRb.AddForce(direction, ForceMode.Impulse);
        objectRb.AddTorque(xRotation, yRotation, zRotation, ForceMode.Impulse);
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z < zDestroy && !gameManager.isGameOver)
        {
            if (!collisionDetector.isDead)
            {
                gameManager.AddScore(1);
            }
            Destroy(gameObject);
        }

        if (gameManager.isGameOver)
        {
            Destroy(gameObject);
        }
    }
}
