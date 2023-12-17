using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duckFinal : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    SpriteRenderer sprRnd;
    public PlayerCode player;
    int direction;
    public float timer = 2;
    float currTime = 0;
    bool walking;
    public AudioSource quackAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprRnd = GetComponent<SpriteRenderer>();
        direction = 2;
        walking = false;
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;
        if(timer < currTime && walking)
        {
            currTime = 0;
            walking = false;
            anim.SetBool("isWalking", false);
            direction = 2;
        }
        else if(timer < currTime)
        {
            anim.SetBool("isWalking", true);
            direction = Random.Range(0, 2);
            
            currTime = 0;
            walking = true;
        }
        if (direction == 0)
        {
            sprRnd.flipX = false;

            transform.Translate(Vector2.right * 2 * Time.deltaTime);
        }
        else if(direction == 1)
        {
            sprRnd.flipX = true;
            transform.Translate(Vector2.left * 2 * Time.deltaTime);
        }
    }
}
