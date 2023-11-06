using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLifeSpan : MonoBehaviour
{
    public float lifeTime = 1;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Destroy(gameObject, 1f);
    }
}
