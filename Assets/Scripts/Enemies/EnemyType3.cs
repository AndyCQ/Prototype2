using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType3 : MonoBehaviour
{
    public float lookDist = 4;

    public LayerMask GroundWallLayer;
    Rigidbody2D _rigidbody;
    Transform player;
    public Transform castPoint;

    private float startPosition;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public int bulletForce = 500;

    public AudioSource gunshotSFX;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(MoveLoop());
    }
    
    IEnumerator MoveLoop(){
        while (true)
        {
            yield return new WaitForSeconds(.1f);
            if (Vector2.Distance(transform.position, player.position) < lookDist && !gameObject.GetComponent<Monster>().isDead)
            {
                if(player.position.x > transform.position.x && transform.localScale.x < 0 || 
                player.position.x < transform.position.x && transform.localScale.x > 0)
                {
                    transform.localScale *= new Vector2(-1,1);
                }
                yield return new WaitForSeconds(1.5f);
                if (!gameObject.GetComponent<Monster>().isDead)
                {
                    GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                    newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.localScale.x, 0) * bulletForce);
                    gunshotSFX.Play();
                }
            }
            //Platform version
            

            //Distance version
            // else if(transform.position.x >= startPosition+distance || (transform.position.x < (startPosition))){
            //     transform.localScale *= new Vector2(-1,1);
            //     _rigidbody.velocity = new Vector2(speed* transform.localScale.x, _rigidbody.velocity.y);
            // }
            // else{
            //     _rigidbody.velocity = new Vector2(speed* transform.localScale.x, _rigidbody.velocity.y);
            // }
        }
    }
}
