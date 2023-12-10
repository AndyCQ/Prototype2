using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionForce = 10f; // Adjust the force as needed

    void Start()
    {
        Explode();
    }

    void Explode()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 5f); // Adjust the radius as needed

        foreach (Collider2D collider in colliders)
        {
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                Vector2 direction = rb.transform.position - transform.position;
                float distance = direction.magnitude;
                float force = 1 - (distance / 5f); // Adjust the falloff as needed

                rb.AddForce(direction.normalized * explosionForce * force, ForceMode2D.Impulse);
            }
        }
    }
}
