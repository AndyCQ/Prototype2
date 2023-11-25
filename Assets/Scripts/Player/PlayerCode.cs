using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCode : MonoBehaviour
{
    public bool isFlipped = false;
    public bool doubleJump;
    int remainingJumps;
    public float speed = 5;
    public int jumpForce = 500;
    public int currHealth;
    public int maxHealth = 6;
    public int bulletForce = 500;
    public int bulletSpeed = 10;
    public float atkCD_Timer = 1;
    public bool fireStatus = true;

    // for testing
    public int jumpForceIncr = 100;
    public float speedIncr = 2;
    public float atkSpdIncr = .1f;


    public LayerMask groundLayer;
    public Transform feetTrans;
    public bool grounded = false;

    public GameObject bulletPrefab;
    public Transform firePoint;

    private Animator _animator;
    Rigidbody2D _rigidbody;
    SpriteRenderer SR;
    public Color dmgColor;
    public Color Original;

    public float knockbackForce = 10;
    public float KBCounter;
    public float KBTotalTime;
    public bool knockFromRight;


    
//UI
    public int speepBoost_count = 0;
    public int jumpBoost_count = 0;
    public int bulletBoost_count = 0;


    public AudioSource gunshotSFX;
    public AudioSource healSFX;
    public AudioSource pickupSFX;
    public AudioSource hitSFX;

    float xSpeed = 0;

    public static PlayerCode Instance;
    public Renderer PlayerRenderer;
    public bool IsImmune;

    public int ducksInLine = 0;
    public List<GameObject> ducks = new List<GameObject>();

    // ability stuff
    public bool shielded = false;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
        currHealth = maxHealth;
        _animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(KBCounter <= 0){
            xSpeed = Input.GetAxisRaw("Horizontal") * speed;

            if((xSpeed < 0 && transform.localScale.x > 0) || (xSpeed > 0 && transform.localScale.x < 0))
            {
                transform.localScale *= new Vector2(-1,1);
                isFlipped = !isFlipped;
            }
            //print(Time.fixedDeltaTime);
            _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);

            _animator.SetFloat("Speed", Mathf.Abs(xSpeed));
            }
        else{
            if(knockFromRight){
                _rigidbody.velocity = new Vector2(-knockbackForce, knockbackForce);
            }
            else{
                _rigidbody.velocity = new Vector2(knockbackForce, knockbackForce);
            }
            KBCounter -= Time.deltaTime;
        }

    }

    void Update(){

        grounded = Physics2D.OverlapCircle(feetTrans.position, .3f, groundLayer);
        _animator.SetBool("Grounded", grounded);
        if (grounded == true)
        {
            remainingJumps = doubleJump ? 1 : 0;
        }
        if ((Input.GetButtonDown("Jump")) && grounded)
        {
            _rigidbody.AddForce(new Vector2(0, jumpForce));
        }
        if(Input.GetButtonDown("Jump") && !grounded && remainingJumps > 0 && PublicVars.mobility == "DJ")
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x,0);
            _rigidbody.AddForce(new Vector2(0, jumpForce));
            remainingJumps -= 1;
        }
        
        if(Input.GetButtonDown("Fire1") && fireStatus){
            GameObject newBullet;
            newBullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletSpeed,0) * bulletForce *transform.localScale + _rigidbody.velocity);
            gunshotSFX.Play();
            StartCoroutine(atkCD(atkCD_Timer));
        }
        if(currHealth > maxHealth){
            currHealth = maxHealth;
        }
        if(currHealth <= 0){
            Die();
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "EnemyBullet") pickupSFX.Play();
        if (other.tag == "SpeedBoost")
        {
            speed = speed + speedIncr;
            speepBoost_count += 1;
            PublicVars.score += 1;
        }
        if (other.tag == "JumpBoost")
        {
            jumpForce = jumpForce + jumpForceIncr;
            jumpBoost_count += 1;
            PublicVars.score += 1;
        }
        if (other.tag == "BulletBoost")
        {
            atkCD_Timer -= atkSpdIncr;
            bulletBoost_count += 1;
            PublicVars.score += 1;
        }
        if (other.tag == "Heal")
        {
            currHealth += 1;
            healSFX.Play();
            PublicVars.score += 1;
        }
        if (other.tag == "EnemyBullet" && shielded == false)
        {
                Destroy(other.gameObject);
                Damage(1);
        }

        if (other.tag == "XP")
        {
            PublicVars.total_xp += 5;
            Destroy(other.gameObject);
        }
        // PublicVars.score += 1;
    }

    void Die() {
        SceneManager.LoadScene("Level1");
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Enemy") && shielded == false)
        {
            Damage(1);
            if(other.gameObject.transform.position.x <= transform.position.x){
                knockFromRight = false;
            }
            else{
                knockFromRight = true;
            }
            KBCounter = KBTotalTime;

            //gameObject.GetComponent<Animation>().Play("GetHit");
        }
    }
    

    public void Damage(int dmg){
        if(!IsImmune){
            currHealth -= dmg;
            //StartCoroutine(hit());
            hitSFX.Play();
        }
    }

    IEnumerator hit(){
        SR.color = dmgColor;
        yield return new WaitForSeconds(.5f);
        SR.color = Original;
    }    

    private IEnumerator atkCD(float timer){
        fireStatus = false;
        yield return new WaitForSeconds(timer);
        fireStatus = true;
    }
}

    
                


