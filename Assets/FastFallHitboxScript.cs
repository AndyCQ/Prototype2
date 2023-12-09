using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastFallHitboxScript : MonoBehaviour
{
    public float ySpeedKillThreashold = 0;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Monster monster = collision.gameObject.GetComponent<Monster>();
        if (monster != null && transform.parent.GetComponent<Rigidbody2D>().velocity.y <= ySpeedKillThreashold)
        {
            monster.Die();
        }
    }
}
