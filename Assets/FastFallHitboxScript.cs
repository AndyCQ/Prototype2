using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastFallHitboxScript : MonoBehaviour
{
    private Camera mainCamera;
    public AudioSource _as;
    public AudioSource _as2;
    public GameObject explosion;
    public bool isShield = false;
    private float y_velocity = 0f;

    public float ySpeedKillThreashold = -32.5f;
    public float lightThudThreashold = -2.5f;
    public ParticleSystem impactParticles;

    void Start()
    {
        // Get a reference to the main camera
        mainCamera = Camera.main;
        if (mainCamera != null)
        {
            // Do something with the main camera
            Debug.Log("FastFallHitboxScript: Main camera found: " + mainCamera.name);
        }
        else
        {
            Debug.LogError("FastFallHitboxScript: Main camera not found in the scene!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Monster monster = collision.gameObject.GetComponent<Monster>();
        y_velocity = (!isShield) ? transform.parent.GetComponent<Rigidbody2D>().velocity.y
            : transform.parent.parent.GetComponent<Rigidbody2D>().velocity.y;

        if (y_velocity <= ySpeedKillThreashold)
        {
            if (monster != null) monster.Die();
            _as.Play();
            //GameObject _exp = Instantiate(explosion, transform.position, transform.rotation);
            //_exp.GetComponent<Explosion>().Explode();
            mainCamera.GetComponent<CameraShake>().Shake(0.75f, 0.2f, 0f);
            if (impactParticles != null) impactParticles.Play();
        } else if (y_velocity <= lightThudThreashold && collision.gameObject.layer == 6)
        {
            _as2.Play();
            mainCamera.GetComponent<CameraShake>().Shake(0.25f, 0.075f * Mathf.Min((y_velocity / lightThudThreashold), 1f), 0f);
        }
    }
}