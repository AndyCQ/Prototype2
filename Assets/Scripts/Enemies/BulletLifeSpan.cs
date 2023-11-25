using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLifeSpan : MonoBehaviour
{

    public float lifeTime = 1;
    public AudioSource groundHitSFX;
    private ParticleSystem _ps;

    void Start()
    {
        Destroy(gameObject, lifeTime);
        _ps = gameObject.GetComponent<ParticleSystem>();
    }


    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // Bullet collided with ground, handle the impact
            groundHitSFX.Play();

            // lock it in place.
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.isKinematic = true;

            // turn off collider.
            GetComponent<Collider2D>().enabled = false;

            // fade out sprite.
            StartCoroutine(HandleGroundCollision());
        }
        
    }


    IEnumerator HandleGroundCollision()
    {
        // create particles upon collision w/ground.
        if (_ps != null)
        {
            _ps.Clear(); // why does this work???
        }

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the initial color of the sprite
        Color originalColor = spriteRenderer.color;

        // Calculate the fade speed based on the duration
        float fadeSpeed = 1f / 3f;

        // Gradually decrease the alpha channel over time
        for (float t = 0f; t < 1f; t += Time.deltaTime * fadeSpeed)
        {
            Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(originalColor.a, 0, t));
            spriteRenderer.color = newColor;
            yield return null;
        }

        // Ensure the sprite is fully transparent
        spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

        // Destroy the GameObject after fading
        Destroy(gameObject);
    }


    //private void HandleGroundCollision()
    //{
    //    // Add any logic here for what should happen when the bullet hits the ground
    //    // For example, destroy the bullet GameObject
    //    Destroy(gameObject, 1f);
    //}

}
