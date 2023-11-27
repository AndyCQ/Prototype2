using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBulletLifeSpan : MonoBehaviour
{
    public float lifeTime = 1;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            HandleGroundCollision();
        }
    }

    private void HandleGroundCollision()
    {
       Destroy(gameObject);
    }
}
