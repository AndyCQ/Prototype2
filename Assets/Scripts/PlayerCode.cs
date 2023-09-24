using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCode : MonoBehaviour
{
    public int speed = 5;
    public int jumpForce = 500;
    public int currHealth;
    public int maxHealth = 6;
    public int bulletForce = 500;
    public int bulletSpeed = 10;

    public LayerMask groundLayer;
    public Transform feetTrans;
    public bool grounded = false;

    public GameObject bulletPrefab;
    public Transform firePoint;

    Rigidbody2D _rigidbody;

    

    float xSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        currHealth = maxHealth;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        xSpeed = Input.GetAxisRaw("Horizontal") * speed;

        if((xSpeed < 0 && transform.localScale.x > 0) || (xSpeed > 0 && transform.localScale.x < 0))
        {
            transform.localScale *= new Vector2(-1,1);
        }
        //print(Time.fixedDeltaTime);
        _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);

    }

    void Update(){
        grounded = Physics2D.OverlapCircle(feetTrans.position, .3f, groundLayer);
        if((Input.GetButtonDown("Jump")) && grounded)
        {
            _rigidbody.AddForce(new Vector2(0, jumpForce));    
        }
        
        if(Input.GetButtonDown("Fire1")){
            GameObject newBullet;
            newBullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletSpeed,0) * bulletForce *transform.localScale);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "SpeedBoost")
        {
            speed = speed + 2;
        }
        if (other.tag == "JumpBoost")
        {
            jumpForce = jumpForce +100;
        }
        if (other.tag == "BulletBoost")
        {
            bulletSpeed = bulletSpeed + 3;
        }
        if (other.tag == "Heal")
        {
            if (currHealth < maxHealth)
            {
                currHealth = currHealth + 1;
            }
        }

    }
}

    
                


