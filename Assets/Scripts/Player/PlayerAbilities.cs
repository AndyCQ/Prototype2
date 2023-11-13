using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{

    //Knockback ability
    public GameObject powerRadiusVisual;
    private GameObject activePowerRadiusVisual;
    public float powerRadius = 10;
    private bool cooldown1 = true;

    public float knockbackForce = 10;
    public float KBCounter;
    public float KBTotalTime;
    public bool knockFromRight;

    //Poison Darts ability
    public GameObject dart;
    public int bulletCount = 8;
    public float shootInterval = 1.0f;
    public float dartSpeed = 5;
    public bool cooldown2 = true;

    // Shield abbibitity
    public float shieldCooldown = 1f;
    public float shieldDuration = 5f;
    public bool cooldown3 = true;

    void Update(){

        if(Input.GetKeyDown(KeyCode.E) && cooldown1){
            KnockbackEnemies();
            StartCoroutine(pushBackCD(2));
        }
        if(Input.GetKeyDown(KeyCode.F) && cooldown2){
            SpawnDarts();
            StartCoroutine(poisonCD(2));
        }
        if (Input.GetKeyDown(KeyCode.S) && cooldown3)
        {
            transform.Find("Shield").gameObject.SetActive(true);
            gameObject.GetComponent<PlayerCode>().shielded = true;
            StartCoroutine(shieldCD(shieldCooldown));
            StartCoroutine(DisableShieldAfterSecs(shieldDuration));
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

    private IEnumerator pushBackCD(float timer){
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