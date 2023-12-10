using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastFallHitboxScript : MonoBehaviour
{
    private Camera mainCamera;
    public AudioSource _as;

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

    public float ySpeedKillThreashold = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Monster monster = collision.gameObject.GetComponent<Monster>();
        if (transform.parent.GetComponent<Rigidbody2D>().velocity.y <= ySpeedKillThreashold)
        {
            mainCamera.GetComponent<CameraShake>().Shake(0.5f, 0.15f, 0f);
            if (monster != null) monster.Die();
            _as.Play();
        }
    }
}
