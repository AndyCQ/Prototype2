using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuckTrap : MonoBehaviour
{
    private PlayerCode player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCode>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") && player.shielded == false){
            StartCoroutine(Trap());

        }
    }

    IEnumerator Trap(){
        int jmpfr = player.jumpForce;
        float spd = player.speed;
        player.jumpForce = 0;
        player.speed = 0;
        yield return new WaitForSeconds(5);
        player.jumpForce = jmpfr;
        player.speed = spd;
        Destroy(gameObject);
    }
}
