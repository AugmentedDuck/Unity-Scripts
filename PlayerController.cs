using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private readonly float speed = 750f;
    private readonly float zBound = 5;
    private readonly float xBound = 8;
    private Rigidbody playerRb;
    public GameObject laser;
    private float timeToShoot = 2f;
    private float timeLeft = 0f;
    public TextMeshProUGUI reloadText;
    private GameManager gameManager;

    public int axisError;
    public int directionError;
    private int laserErrorCode = 10;
    private bool isLaserJammed = false;

    private AudioSource audioSource;
    public AudioClip laserShot;
    public AudioClip errorSound;

    public bool isShooting = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        playerRb.AddForce(60 * horizontalInput * speed * Time.deltaTime * Vector3.right);
        playerRb.AddForce(60 * speed * Time.deltaTime * verticalInput * Vector3.forward);

        StopAtBounds();
        ShootLaser();
        LaserError();
        ShipError();
    }

    void StopAtBounds()
    {
        if (transform.position.z < -zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);
            playerRb.velocity = Vector3.zero;
        }

        if (transform.position.z > zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
            playerRb.velocity = Vector3.zero;
        }

        if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
            playerRb.velocity = Vector3.zero;
        }

        if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
            playerRb.velocity = Vector3.zero;
        }
    }

    void ShootLaser()
    {
        if (isLaserJammed)
        {
            reloadText.text = "JAMMED!";
        }
        else if (timeLeft <= 0)
        {
            reloadText.text = " ";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(laser, transform.position, laser.transform.rotation);
                timeLeft = timeToShoot;
                laserErrorCode = Random.Range(0, 10);
                audioSource.PlayOneShot(laserShot);
            }
        }
        else if (timeLeft <= timeToShoot * 1 / 4)
        {
            timeLeft -= 1f * Time.deltaTime;
            reloadText.text = "RELOADING...";

        }
        else if (timeLeft <= timeToShoot * 2 / 4)
        {
            timeLeft -= 1f * Time.deltaTime;
            reloadText.text = "RELOADING..";
        }
        else if (timeLeft <= timeToShoot * 3 / 4)
        {
            timeLeft -= 1f * Time.deltaTime;
            reloadText.text = "RELOADING.";
        }
        else if (timeLeft <= timeToShoot)
        {
            timeLeft -= 1f * Time.deltaTime;
            reloadText.text = "RELOADING";
        }
    }

    void LaserError()
    {
        if (laserErrorCode == 1 && !isLaserJammed)
        {
            isLaserJammed = true;
            gameManager.isError = true;
            audioSource.PlayOneShot(errorSound);
        }
        else if (laserErrorCode == 1)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                laserErrorCode = 0;
                isLaserJammed = false;
                gameManager.isError = false;
            }
        }
    }

    void ShipError()
    {
        if (gameManager.gameErrorCode == 1 && !gameManager.isError)
        {
            axisError = Random.Range(0, 2);
            directionError = Random.Range(-1, 2);
            gameManager.isError = true;
            audioSource.PlayOneShot(errorSound);
        }
        else if (gameManager.gameErrorCode == 1)
        {
            if (axisError == 0)
            {
                playerRb.AddForce(Vector3.right * speed / 4 * directionError * Time.deltaTime * 60);
            }
            else
            {
                playerRb.AddForce(Vector3.forward * speed / 4 * directionError * Time.deltaTime * 60);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                gameManager.isError = false;
                gameManager.ShipError();
            }
        }
    }
}
