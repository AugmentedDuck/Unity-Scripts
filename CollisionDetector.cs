using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private AudioSource audioSource;
    private GameManager gameManager;
    private GameObject mesh;
    private Rigidbody RbObject;
    private ParticleSystem particles;
    public bool isDead = false;

    public AudioClip asteroidHit;
    public AudioClip grenadeHit;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        audioSource = GetComponent<AudioSource>();
        mesh = gameObject.transform.GetChild(0).gameObject;
        RbObject = GetComponent<Rigidbody>();
        particles = GetComponent<ParticleSystem>();

        mesh.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile") && !gameManager.isGameOver)
        {
            PlayGrenadeHit();
            Destroy(collision.gameObject);
            mesh.SetActive(false);

            RbObject.detectCollisions = false;
            RbObject.freezeRotation = true;
            RbObject.AddForce(0, 0, 1000f, ForceMode.Impulse);
            
            particles.Play();
            
            isDead = true;
            
            gameManager.AddScore(1);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            gameManager.DamageLives(1);
            PlayAsteroidHit();
            //Destroy(gameObject);
        }
    }

    public void PlayAsteroidHit()
    {
        audioSource.PlayOneShot(asteroidHit);
    }

    public void PlayGrenadeHit()
    {
        audioSource.PlayOneShot(grenadeHit);
    }
}
