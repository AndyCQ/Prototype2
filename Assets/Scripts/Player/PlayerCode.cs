using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCode : MonoBehaviour
{
    public bool ducksGoBack;
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
    public AudioSource deathSFX;
    public AudioSource backgroundMusic;
    public AudioSource jumpingSFX;
    public AudioSource footStepsSFX;

    float xSpeed = 0;

    public static PlayerCode Instance;
    public Renderer PlayerRenderer;
    public bool IsImmune;

    public int ducksInLine = 0;
    public List<GameObject> ducks = new List<GameObject>();

    // ability stuff
    public bool shielded = false;
    public bool stunned = false;
    public bool isDead = false;

    private Camera mainCamera;

    public bool bossCut;
    public bool bossCutFinished = false;
    public bool stopFollowing;

    
    public GameObject goose;
    public bool hasGoose;
    // Start is called before the first frame update
    void Start()
    {
        stopFollowing = false;
        bossCut = false;
        ducksGoBack = false;
        _rigidbody = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        speed = PublicVars.starting_spd;
        jumpForce = PublicVars.starting_jumpforce;
        atkCD_Timer = PublicVars.starting_atk_spd;
        maxHealth = PublicVars.starting_health;

        currHealth = maxHealth;

        mainCamera = Camera.main;
        hasGoose = false;

        string currentSceneName = SceneManager.GetActiveScene().name;
        PublicVars.currLevel = currentSceneName;
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
        if (isDead) return;
        if(KBCounter <= 0){
            if (!bossCut)
            {
                xSpeed = Input.GetAxisRaw("Horizontal") * speed;
            }

            if ((xSpeed < 0 && transform.localScale.x > 0) || (xSpeed > 0 && transform.localScale.x < 0))
            {
                transform.localScale *= new Vector2(-1,1);
                isFlipped = !isFlipped;
            }
            //print(Time.fixedDeltaTime);
            if (!bossCut)
            {
                _rigidbody.velocity = new Vector2(xSpeed, _rigidbody.velocity.y);
            }

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

    private bool lastFrameGrounded;
    void Update(){
        if(bossCutFinished && mainCamera.orthographicSize < 16.87695f)
        {
            mainCamera.orthographicSize += Time.deltaTime * 5;
        }
        if (hasGoose && gameObject.transform.position.x > -20 && !bossCut && !bossCutFinished)
        {
            bossCut = true;
        }
        if (bossCut && goose.transform.position.x < -2)
        {
            if(mainCamera.orthographicSize > 10)
            {
                mainCamera.orthographicSize -= Time.deltaTime * 2f;
            }
            _rigidbody.velocity = new Vector2(speed, _rigidbody.velocity.y);
            xSpeed = speed;
        }
        else if (bossCut)
        {
            xSpeed = 0;
            _rigidbody.velocity = new Vector2(0, _rigidbody.velocity.y);
            if (!isFlipped)
            {
                transform.localScale *= new Vector2(-1, 1);
                isFlipped = !isFlipped;
            }

            stopFollowing = true;

        }
        if (isDead) return;
        grounded = Physics2D.OverlapCircle(feetTrans.position, .3f, groundLayer);
        _animator.SetBool("Grounded", grounded);
        if (!grounded && footStepsSFX.isPlaying) footStepsSFX.Stop();
        if (grounded == true)
        {
            remainingJumps = doubleJump ? 1 : 0;
            if (_rigidbody.velocity.x == 0f) { footStepsSFX.volume = 0.05f; }
            else { footStepsSFX.volume = 0.35f; }
            if (_rigidbody.velocity.x != 0f && !footStepsSFX.isPlaying) { footStepsSFX.Play(); }
        }
        if ((Input.GetButtonDown("Jump")) && grounded && !bossCut)
        {
            if (Time.timeScale != 0f) _rigidbody.AddForce(new Vector2(0, jumpForce));
            jumpingSFX.Play();
        }
        if(Input.GetButtonDown("Jump") && !grounded && remainingJumps > 0 && PublicVars.mobility == "DJ" && !bossCut)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x,0);
            _rigidbody.AddForce(new Vector2(0, jumpForce));
            remainingJumps -= 1;
            jumpingSFX.Play();
        }

        if (Input.GetKeyDown(KeyCode.S) && !grounded)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
            _rigidbody.AddForce(new Vector2(0, -1500));
            jumpingSFX.Play();
        }

        if (Input.GetButtonDown("Fire1") && fireStatus){
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
        if(transform.position.y < -20){
            Die();
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //if (other.tag != "EnemyBullet") pickupSFX.Play();
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
        }
        if (other.tag == "EnemyBullet" && shielded == false)
        {
                Destroy(other.gameObject);
                Damage(1);
        }

        if (other.tag == "XP")
        {
            pickupSFX.Play();
            PublicVars.total_xp += 1;
            Destroy(other.gameObject);
        }
        if(other.tag == "LightingBolt" && shielded == false){
            KB(other);
        }
        if(other.tag == "Boss" && shielded == false){
            KB(other);
        }
        if(other.tag == "StunBullet" && shielded == false){
            Damage(1);
            StartCoroutine(Stun());
        }


        // PublicVars.score += 1;
    }
    void KB(Collider2D other){
        if(other.gameObject.transform.position.x <= transform.position.x){
            knockFromRight = false;
        }
        else{
            knockFromRight = true;
        }
        KBCounter = KBTotalTime;
        Damage(1);
    }

    public void Die() {
        isDead = true;
        backgroundMusic.Stop();
        deathSFX.Play();
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<PlayerDamage>().enabled = false;
        SR.enabled = false;
        _rigidbody.constraints = RigidbodyConstraints2D.FreezePosition;

        Transform deathPiecesTransform = gameObject.transform.Find("death_pieces");
        if (deathPiecesTransform != null) deathPiecesTransform.gameObject.SetActive(true);

        StartCoroutine(LoadLevelAfterDelay(5.1f));
    }

    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Enemy") && shielded == false)
        {
            if(other.gameObject.transform.position.x <= transform.position.x){
                print("here");
                knockFromRight = false;
            }
            else{
                knockFromRight = true;
            }
            KBCounter = KBTotalTime;
            Damage(1);
            //gameObject.GetComponent<Animation>().Play("GetHit");
        }
    }
    

    public void Damage(int dmg){
        if(!IsImmune){
            currHealth -= dmg;
            //StartCoroutine(hit());
            hitSFX.Play();
            mainCamera.GetComponent<CameraShake>().Shake(0.5f, 0.1f, 0f);
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

    IEnumerator Stun(){
        if(!stunned){
            int jmpfr = jumpForce;
            float spd = speed;
            stunned = true;
            jumpForce = 0;
            speed = 0;
            yield return new WaitForSeconds(2);
            jumpForce = jmpfr;
            speed = spd;
        }
    }

}

    
                


