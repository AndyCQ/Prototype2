using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{   
    public float speed = 4;
    public float startSpd = 4;
    public int currHealth;
    public int maxHealth = 4;
    public Rigidbody2D Mrb;
    public bool moving = true;

    void Start(){
        Mrb = GetComponent<Rigidbody2D>();
        speed = startSpd;
        currHealth = maxHealth;
    }

    void Update(){
        if(currHealth <= 0){
            Die();
            }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bullet")){
            Destroy(other.gameObject);
            currHealth -= PublicVars.bulletDMG;
        }
        if (other.CompareTag("PoisonDart")){
            Destroy(other.gameObject);
            StartCoroutine(DoT());
        }
    }

    void Die() {
        Destroy(gameObject,.15f);
    }

    IEnumerator DoT(){
        for (int i = 0; i < 6; i++) 
        {
            //Tickrate
            yield return new WaitForSeconds(1);
            currHealth -= 1;
        }

    }

}