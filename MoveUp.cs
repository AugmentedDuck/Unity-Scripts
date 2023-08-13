using UnityEngine;

public class MoveUp : MonoBehaviour
{
    public float speed = 5.0f;
    private readonly float zDestroy = 8.0f;
    private Rigidbody objectRb;
    private readonly float rotationRange = 1f;

    // Start is called before the first frame update
    void Start()
    {
        float xRotation = Random.Range(-rotationRange, rotationRange);
        float yRotation = Random.Range(-rotationRange, rotationRange);
        float zRotation = Random.Range(-rotationRange, rotationRange);

        objectRb = GetComponent<Rigidbody>();
        objectRb.AddForce(Vector3.forward * speed, ForceMode.Impulse);
        objectRb.AddTorque(xRotation, yRotation, zRotation, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.z > zDestroy)
        {
            Destroy(gameObject);
        }
    }
}
