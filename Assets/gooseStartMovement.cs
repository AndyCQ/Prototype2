using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gooseStartMovement : MonoBehaviour
{
    Animator anim;

    public bool battleBegin = false;
    public GameObject target;
    bool finished;
    bool following;
    Vector3 offset;
    public PlayerCode player;
    Rigidbody2D rb;
    SpriteRenderer sprRnd;
    float newScale;
    public float growthSpeed;
    public GameObject bossHealthBar;
    bool positionSet;
    // Start is called before the first frame update

    // backgroundMusic
    public AudioSource playerMusic;
    public string angryMusicPath;
    private AudioClip angryMusicClip;

    void Start()
    {
        sprRnd = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        offset = new Vector3(0, -0.85f, 0);
        positionSet = false;
        angryMusicClip = Resources.Load<AudioClip>(angryMusicPath);
    }

    private bool alreadyPlaying = false;
    private bool alreadyStopped = false;
    // Update is called once per frame
    void Update()
    {
        if(player.stopFollowing)
        {
            if (!alreadyStopped)
            {
                playerMusic.Stop();
                alreadyStopped = true;
            }
            rb.velocity = Vector3.zero;
            following = false;
            //Debug.Log("done");
            if(transform.localScale.x < 7.1)
            {
                transform.localScale = new Vector3(transform.localScale.x + Time.deltaTime*1, transform.localScale.y + Time.deltaTime*1, transform.localScale.z);
            }
            if(transform.position.y < 4)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * 1f, transform.position.z);

            }
            else
            {
                positionSet = true;
            }
        }
        if (positionSet)
        {
            bossHealthBar.SetActive(true);
            player.bossCut = false;
            player.bossCutFinished = true;
            gameObject.tag = "Boss";
            battleBegin = true;
            playerMusic.clip = angryMusicClip;
            playerMusic.volume = 0.5f;
            if (!alreadyPlaying)
            {
                playerMusic.Play();
                alreadyPlaying = true;
            }
        }
        if (!battleBegin && following)
        {
            if (player.isFlipped)
            {
                offset.x = 5f;
            }
            else
            {
                offset.x = -5f;
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
                //anim.SetBool("isWalking", true);
                rb.velocity = dif * 2f;
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
        if (col.gameObject.CompareTag("Player") && !finished)
        {
            target = col.gameObject;
            following = true;
            player.hasGoose = true;
        }
    }

}
