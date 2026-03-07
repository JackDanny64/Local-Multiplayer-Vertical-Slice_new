using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 30f;
    public int damage = 20;
    public float lifetime = 3f;

    // NEW: store the shooter’s player ID
    public int shooterID;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();

        if (health != null)
        {
            // Pass the shooterID so the correct player gets the point
            health.TakeDamage(damage, shooterID);
        }

        Destroy(gameObject);
    }
}