using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public float speed = 3;
    public float lookDist = 5;

    public float leftBoundary = -10.0f;
    public float rightBoundary = 10.0f;
    private int direction = 1;
    public float spawnPoint;

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
        spawnPoint = transform.position.x;
        StartCoroutine(MoveLoop());
        
    }

    IEnumerator MoveLoop(){
        while(true){
            float newPosition = transform.position.x + direction * speed;
            if (newPosition > spawnPoint + rightBoundary)
            {
                transform.localScale *= new Vector2(-1,1);
            }
            else if (newPosition < spawnPoint + leftBoundary)
            {
                transform.localScale *= new Vector2(-1,1);
            }
            _rigidbody.velocity = new Vector2(speed* transform.localScale.x, _rigidbody.velocity.y);

            //Firerate
            yield return new WaitForSeconds(1.5f);
            
            //Aim shot
            if (Vector2.Distance(transform.position, player.position) < lookDist && !gameObject.GetComponent<Monster>().isDead)
            {
                GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation * Quaternion.Euler(0,0,90));                
                newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2((player.position.x - transform.position.x),(player.position.y - transform.position.y)) * bulletForce);   
            }
        }
    }


}
