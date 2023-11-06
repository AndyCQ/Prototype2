using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperType : MonoBehaviour
{
    public float speed = 3;
    public float lookDist = 5;

    public int currHealth;
    public int maxHealth = 3;

    Rigidbody2D _rigidbody;
    Transform player;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public int bulletForce = 500;

    void Start()
    {
        currHealth = maxHealth;
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(Aim());
        
    }

    IEnumerator Aim(){
        while(true){
            if(player.position.x > transform.position.x && transform.localScale.x < 0 || 
                player.position.x < transform.position.x && transform.localScale.x > 0)
                {
                    transform.localScale *= new Vector2(-1,1);
                }

            //Firerate
            yield return new WaitForSeconds(1.5f);
            
            //Aim shot
            if (Vector2.Distance(transform.position, player.position) < lookDist){
                GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation * Quaternion.Euler(0,0,90));                
                newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2((player.position.x - transform.position.x),(player.position.y - transform.position.y)) * bulletForce);   
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bullet")){
            Destroy(other.gameObject);
            currHealth -= PublicVars.bulletDMG;
        }
        if(currHealth <= 0){
                Die();
            }
    }

    void Die() {
        Destroy(gameObject,.15f);
    }

}
