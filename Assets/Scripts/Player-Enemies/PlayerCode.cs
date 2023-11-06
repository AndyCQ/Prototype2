using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCode : MonoBehaviour
{
    public bool doubleJump;
    int remainingJumps;
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
    SpriteRenderer SR;
    public Color dmgColor;
    public Color Original;

    public int knockbackForce = 10;
    
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

    //Knockback ability
    public GameObject powerRadiusVisual;
    private GameObject activePowerRadiusVisual;
    public float powerRadius = 10;
    public float thrust = 300;
    private bool cooldown1 = true;

    //Poison Darts ability
    public GameObject dart;
    public int bulletCount = 8;
    public float shootInterval = 1.0f;
    public float dartSpeed = 20;
    public bool cooldown2 = true;


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
        currHealth = maxHealth;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        
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
        if (grounded == true)
        {
            remainingJumps = doubleJump ? 1 : 0;
        }
        if ((Input.GetButtonDown("Jump")) && grounded)
        {
            _rigidbody.AddForce(new Vector2(0, jumpForce));
        }
        if(Input.GetButtonDown("Jump") && !grounded && remainingJumps > 0)
        {
            _rigidbody.AddForce(new Vector2(0, jumpForce));
            remainingJumps -= 1;
        }

        
        if(Input.GetButtonDown("Fire1")){
            GameObject newBullet;
            newBullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletSpeed,0) * bulletForce *transform.localScale);
            gunshotSFX.Play();
        }

        if(Input.GetKeyDown(KeyCode.E) && cooldown1){
            KnockbackEnemies();
            StartCoroutine(cooldown(2));
        }
        if(Input.GetKeyDown(KeyCode.F) && cooldown2){
            SpawnDarts();
        }
        if(currHealth > maxHealth){
            currHealth = maxHealth;
            StartCoroutine(PdartsCD(2));
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
            speed = speed + 2;
            speepBoost_count += 1;
        }
        if (other.tag == "JumpBoost")
        {
            jumpForce = jumpForce +100;
            jumpBoost_count += 1;
        }
        if (other.tag == "BulletBoost")
        {
            bulletSpeed = bulletSpeed + 3;
            bulletBoost_count += 1;
        }
        if (other.tag == "Heal")
        {
            currHealth += 1;
            healSFX.Play();
        }
        if (other.tag == "EnemyBullet"){
                Destroy(other.gameObject);
                Damage(1);
                }
        PublicVars.score += 1;
    }

    void Die() {
        SceneManager.LoadScene("EndGame");
    }

    private void OnCollisionEnter2D(Collision2D other) {
        Collider2D collider = other.collider;
        if(other.gameObject.CompareTag("Enemy"))
        {
            Damage(1);
            //gameObject.GetComponent<Animation>().Play("GetHit");
            Vector2 direction = new Vector2(1, 1).normalized;
            Vector2 knockback = direction * knockbackForce;
            knockbacked(knockback);
        }
    }
    
    public void knockbacked(Vector2 knockback){
        _rigidbody.AddForce(knockback, ForceMode2D.Impulse);

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

    private void KnockbackEnemies(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, powerRadius);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                StartCoroutine(PushBack(collider));
            }
        }
    }
    private IEnumerator PushBack(Collider2D collider){
        Monster mc = collider.gameObject.GetComponent<Monster>();
        mc.moving = false;
        mc.speed = 0;
        Vector2 knockbackDirection = (collider.transform.position - transform.position).normalized;
        mc.Mrb.AddForce(knockbackDirection * thrust, ForceMode2D.Impulse);
        print(knockbackDirection * thrust);
        yield return new WaitForSeconds(.5F);
        mc.Mrb.velocity = new Vector2(0f,0f);
        mc.speed = mc.startSpd;
        mc.moving = true;
        }

    //Combine soon
    private IEnumerator cooldown(float timer){
        cooldown1 = false;
        yield return new WaitForSeconds(timer);
        cooldown1 = true;
    }

    private IEnumerator PdartsCD(float timer){
        cooldown1 = false;
        yield return new WaitForSeconds(timer);
        cooldown1 = true;
    }

    void SpawnDarts(){
        float angleStep = 360f / bulletCount;
        float currentAngle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            // Calculate the position for each bullet.
            float radians = currentAngle * Mathf.Deg2Rad;
            Vector2 spawnPosition = new Vector2(transform.position.x + Mathf.Cos(radians), transform.position.y + Mathf.Sin(radians));

            // Calculate the direction for each bullet.
            Vector2 spawnDirection = (spawnPosition - (Vector2)transform.position).normalized;

            // Create a new bullet at the calculated position.
            GameObject bullet = Instantiate(dart, spawnPosition, Quaternion.identity);

            // Set the bullet's velocity to move in the calculated direction.
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = spawnDirection * dartSpeed;
            }

            currentAngle += angleStep;
        }
    }

    
}

    
                


