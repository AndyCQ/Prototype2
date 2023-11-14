using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class duckKey : MonoBehaviour
{
    Animator anim;
    public GameObject target;
    bool finished;
    bool following;
    bool foundSpot = false;
    Rigidbody2D rb;
    SpriteRenderer sprRnd;
    Vector3 offset;
    public PlayerCode player;
    int spotInLine = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprRnd = GetComponent<SpriteRenderer>();
        offset = new Vector3(0, -0.85f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        

        if (following && !finished)
        {
            if (player.isFlipped)
            {
                offset.x = 1.5f;
            }
            else
            {
                offset.x = -1.5f;
            }
            var dif = target.transform.position - transform.position + offset;
            var dist = target.transform.position - transform.position;
            if (dist.x < 0)
            {
                sprRnd.flipX = true;
            }
            if (dist.x > 0)
            {
                sprRnd.flipX = false;
            }

            if (dif.magnitude > 0.2)
            {
                anim.SetBool("isWalking", true);
                rb.velocity = dif * 5f;
            }
            if (dif.magnitude <= 0.2)
            {
               // Debug.Log("doing the thing");
                anim.SetBool("isWalking", false);
                rb.velocity = Vector3.zero;
            }
            //Debug.Log(dif.magnitude);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("collided");
        /*if (col.gameObject.CompareTag("Portal") && !finished)
        {
            finished = true;
            target = col.gameObject;
        }*/
        if (col.gameObject.CompareTag("Player") && !finished)
        {
            following = true;
            if (!foundSpot)
            {
                spotInLine = player.ducksInLine;
                player.ducksInLine++;
                foundSpot = true;
                player.ducks.Add(this.gameObject);
                if (spotInLine == 0)
                {
                    offset.y = -0.85f;
                    target = col.gameObject;

                }
                else
                {
                    target = player.ducks[spotInLine-1];
                    offset.y = 0;
                }
            }
        }
    }
}
