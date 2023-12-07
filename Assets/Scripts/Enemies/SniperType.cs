using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperType : MonoBehaviour
{
    public float speed = 3;
    public float lookDist = 5;


    Rigidbody2D _rigidbody;
    Transform player;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public int bulletForce = 500;

    void Start()
    {
        
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(MoveLoop());
        
    }

    IEnumerator MoveLoop(){
        while(true){
            if(player.position.x > transform.position.x && transform.localScale.x < 0 || 
                player.position.x < transform.position.x && transform.localScale.x > 0)
                {
                    transform.localScale *= new Vector2(-1,1);
                }

            //Firerate
            yield return new WaitForSeconds(1.5f);
            
            //Aim shot
            if (Vector2.Distance(transform.position, player.position) < lookDist && !gameObject.GetComponent<Monster>().isDead)
            {
                GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation * Quaternion.Euler(0,0,0));                
                newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2((player.position.x - transform.position.x),(player.position.y - transform.position.y)) * bulletForce);

                float angle = Mathf.Atan2(player.position.y - transform.position.y, player.position.x - transform.position.x) * Mathf.Rad2Deg;
                newBullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
        }
    }

    

}
