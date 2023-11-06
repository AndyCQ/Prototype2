using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType2 : MonoBehaviour
{
    public float distance = 4;
    public float lookDist = 4;

    public int jumpForce = 500;
    public bool grounded = false;


    public LayerMask GroundWallLayer;
    Rigidbody2D _rigidbody;
    Transform player;
    public Transform castPoint;
    public Transform feetTrans;

    public float knockbackPower = 4;
    private PlayerCode spaceman;

    Monster mc;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(MoveLoop());
        mc = GetComponent<Monster>();
    }
    
    IEnumerator MoveLoop(){
        while (true)
        {
            yield return new WaitForSeconds(.1f);
            if (Vector2.Distance(transform.position, player.position) < lookDist){
                if(player.position.x > transform.position.x && transform.localScale.x < 0 || 
                player.position.x < transform.position.x && transform.localScale.x > 0)
                {
                    transform.localScale *= new Vector2(-1,1);
                }
                // grounded = Physics2D.OverlapCircle(feetTrans.position, .3f, GroundWallLayer);
                // if(grounded){
                //     yield return new WaitForSeconds(1);
                //     
                // // }
                // yield return new WaitForSeconds(Random.Range(1,5));
                // _rigidbody.AddForce(new Vector2(0, jumpForce));
                  
            }
            //Platform version
            else if (Physics2D.Raycast(castPoint.position, transform.forward, 1, GroundWallLayer) ||
            !Physics2D.Raycast(castPoint.position,-transform.up,1,GroundWallLayer))
            {
                transform.localScale *= new Vector2(-1,1);
            }

            _rigidbody.velocity = new Vector2(mc.speed* transform.localScale.x, _rigidbody.velocity.y);
        }
    }
  


    // private void OnCollisionEnter2D(Collision2D other)
    // {
    //     if (other.gameObject.CompareTag("Player")){
    //         spaceman.Damage(3);
    //         StartCoroutine(spaceman.Knockback(0.02f, knockbackPower, spaceman.transform.position));
    //     }
    // }

}
