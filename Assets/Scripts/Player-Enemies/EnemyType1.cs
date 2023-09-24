using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : MonoBehaviour
{
    public float speed = 4;
    public float distance = 4;
    public float lookDist = 4;

    public int currHealth;
    public int maxHealth = 4;

    public LayerMask GroundWallLayer;
    Rigidbody2D _rigidbody;
    Transform player;
    public Transform castPoint;

    private float startPosition;
    private float originalXPos;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public int bulletForce = 500;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(MoveLoop());
        
        startPosition = transform.position.x;
    }
    
    IEnumerator MoveLoop(){
        while (true)
        {
            yield return new WaitForSeconds(.1f);

            //Platform version
            if(Physics2D.Raycast(castPoint.position, transform.forward, 1, GroundWallLayer) ||
            !Physics2D.Raycast(castPoint.position,-transform.up,1,GroundWallLayer)){
                transform.localScale *= new Vector2(-1,1);
                _rigidbody.velocity = new Vector2(speed* transform.localScale.x, _rigidbody.velocity.y);
            }
            else{
                _rigidbody.velocity = new Vector2(speed* transform.localScale.x, _rigidbody.velocity.y);
            }

            // //Distance version
            // else if(transform.position.x >= startPosition+distance || (transform.position.x < (startPosition))){
            //     transform.localScale *= new Vector2(-1,1);
            //     _rigidbody.velocity = new Vector2(speed* transform.localScale.x, _rigidbody.velocity.y);
            // }
            // else{
            //     _rigidbody.velocity = new Vector2(speed* transform.localScale.x, _rigidbody.velocity.y);
            // }
        }
    }
  

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bullet")){
            Destroy(other.gameObject);
            currHealth -= PublicVars.bulletDMG;
        if(currHealth <= 0){
                Die();
            }
    }

    void Die() {
        PublicVars.score += 1;

        Destroy(gameObject,.15f);
        print(PublicVars.score);
    }
    }
}
