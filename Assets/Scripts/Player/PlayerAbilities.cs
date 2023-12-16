using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{

    //Knockback ability
    public float powerRadius = 10;
    private bool cooldown1 = true;

    public float knockbackForce = 10;
    public float KBCounter;
    public float KBTotalTime;
    public bool knockFromRight;
    private int knockbackDucks = 6;
    public GameObject knockbackDuck;
    public int knockbackDuckspeed = 5;

    //Poison Darts ability
    public GameObject dart;
    private int bulletCount = 12;
    public float shootInterval = 1.0f;
    public float dartSpeed = 5;
    public bool cooldown2 = true;

    // Shield ability
    public float shieldCooldown = 1f;
    public float shieldDuration = 5f;
    public bool cooldown3 = true;

    //Railgun ability
    public GameObject railshot;
    public float railSpeed = 5;
    public Transform firePoint;
    public Rigidbody2D rb;
    public int totalParts = 100;  // Total number of parts to instantiate
    //public bool cooldown3 = true;

    
    void Update(){

        if(Input.GetKeyDown(KeyCode.E) && cooldown1){
            activate();
            // KnockbackEnemies();
            // StartCoroutine(pushBackCD(2));
        }
        if(Input.GetKeyDown(KeyCode.Q) && cooldown2 && PublicVars.secondaryFire == "PDs"){
            SpawnDarts();
            StartCoroutine(poisonCD(2));
        }
        // if (Input.GetKeyDown(KeyCode.S) && cooldown3)
        // {
        //     transform.Find("Shield").gameObject.SetActive(true);
        //     gameObject.GetComponent<PlayerCode>().shielded = true;
        //     StartCoroutine(shieldCD(shieldCooldown));
        //     StartCoroutine(DisableShieldAfterSecs(shieldDuration));
        // }
        // if(Input.GetKeyDown(KeyCode.Q)){
        //     GameObject newBullet;
        //     newBullet = Instantiate(railshot, firePoint.position + new Vector3(17,0,0) * transform.localScale.x, Quaternion.identity);
        //     //newBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(railSpeed,0) * 1 *transform.localScale + rb.velocity);
        //     //gunshotSFX.Play();
        //     //StartCoroutine(atkCD(atkCD_Timer));
            
        // }
    }

    private void activate(){
        if(PublicVars.support == "Shield"){
            transform.Find("Shield").gameObject.SetActive(true);
            gameObject.GetComponent<PlayerCode>().shielded = true;
            StartCoroutine(supportCD(shieldCooldown));
            StartCoroutine(DisableShieldAfterSecs(shieldDuration));
        }
        if(PublicVars.support == "Knockback"){
            KnockbackEnemies();
            SpawnKnockBackDucks();
            StartCoroutine(supportCD(2));
        }
    }


    private IEnumerator DisableShieldAfterSecs(float secs)
    {
        yield return new WaitForSeconds(secs);
        transform.Find("Shield").gameObject.SetActive(false);
        gameObject.GetComponent<PlayerCode>().shielded = false;
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
        Rigidbody2D mcR = mc.GetComponent<Rigidbody2D>();
        mc.moving = false;
        float xDir = knockbackForce;
        float yDir = 0;
        if(collider.gameObject.transform.position.x < transform.position.x){
            xDir *= -1;
        }
        if(collider.gameObject.transform.position.y > transform.position.y + 1){
            yDir = knockbackForce;
        }

        mcR.velocity = new Vector2(xDir, yDir);
        yield return new WaitForSeconds(.4f);
        mc.moving = true;
        }


    private IEnumerator supportCD(float timer){
        cooldown1 = false;
        yield return new WaitForSeconds(timer);
        cooldown1 = true;
    }

    private IEnumerator poisonCD(float timer){
        cooldown2 = false;
        yield return new WaitForSeconds(timer);
        cooldown2 = true;
    }

    private IEnumerator shieldCD(float timer)
    {
        cooldown3 = false;
        yield return new WaitForSeconds(timer);
        cooldown3 = true;
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
            GameObject bullet = Instantiate(dart, spawnPosition,  transform.rotation * Quaternion.Euler(0,0,currentAngle));

            // Set the bullet's velocity to move in the calculated direction.
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = spawnDirection * dartSpeed;
            }

            currentAngle += angleStep;
        }

    }
    void SpawnKnockBackDucks(){
        float angleStep = 360f / knockbackDucks;
        float currentAngle = 0f;

        for (int i = 0; i < knockbackDucks; i++)
        {
            currentAngle += angleStep;
            // Calculate the position for each bullet.
            float radians = currentAngle * Mathf.Deg2Rad;
            Vector2 spawnPosition = new Vector2(transform.position.x + Mathf.Cos(radians), transform.position.y + Mathf.Sin(radians));

            // Calculate the direction for each bullet.
            Vector2 spawnDirection = (spawnPosition - (Vector2)transform.position).normalized;

            // Create a new bullet at the calculated position.
            GameObject bullet = Instantiate(knockbackDuck, spawnPosition,  transform.rotation * Quaternion.Euler(0,0,radians));

            // Set the bullet's velocity to move in the calculated direction.
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = spawnDirection * knockbackDuckspeed;
            }

        }

    }

    
}