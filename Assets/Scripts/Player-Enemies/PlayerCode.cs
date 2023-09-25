using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    SpriteRenderer SR;
    public Color dmgColor;
    public Color Original;

    public int knockbackForce = 10;
    
//UI
    public int speepBoost_count = 0;
    public int jumpBoost_count = 0;
    public int bulletBoost_count = 0;
    public int sum_count = 0;

    float xSpeed = 0;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        SR = GetComponent<SpriteRenderer>();
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

        if(currHealth > maxHealth){
            currHealth = maxHealth;
        }
        if(currHealth <= 0){
            Die();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
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
        }
        if (other.tag == "EnemyBullet"){
                Destroy(other.gameObject);
                Damage(1);
                }
        sum_count += 1;
    }

    void Die() {
        SceneManager.LoadScene("RogerScene1");
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
        currHealth -= dmg;
        StartCoroutine(hit());
        
    }

    IEnumerator hit(){
        SR.color = dmgColor;
        yield return new WaitForSeconds(.5f);
        SR.color = Original;
    }
    // public IEnumerator Knockback (float knockDur, float knockbackPwr, Vector3 knockbackDir){
    //     float timer = 0;
    //     print("here");
    //     _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);

    //     while (knockDur > timer) {
    //         timer += Time.deltaTime;
    //         _rigidbody.AddForce(new Vector3(knockbackDir.x + -1000, knockbackDir.y + knockbackPwr, transform.position.z));
    //     }

    //     yield return 0;
    // }

}

    
                


