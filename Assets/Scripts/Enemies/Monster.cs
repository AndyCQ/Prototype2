using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{   
    public float speed;
    public float startSpd = 4;
    public int currHealth;
    public int maxHealth = 4;
    public Rigidbody2D Mrb;
    public bool moving = true;
    public int scoreVal = 1;
    private HealthBar hb;
    public bool splitter = false;

    void Start(){
        Mrb = GetComponent<Rigidbody2D>();
        speed = startSpd;
        currHealth = maxHealth;
        hb = GetComponent<HealthBar>();
    }

    void Update(){
        if(currHealth <= 0){
            Die();
            }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bullet")){
            Destroy(other.gameObject);
            currHealth -= PublicVars.bulletDMG;
            hb.TakeDamage(PublicVars.bulletDMG);
        }
        if (other.CompareTag("PoisonDart")){
            Destroy(other.gameObject);
            StartCoroutine(DoT());
        }
    }

    void Die() {
        PublicVars.score += scoreVal;
        Debug.Log("enemy died, score + 1");
        if(splitter){
            EnemyTypeSplitter parent = GetComponent<EnemyTypeSplitter>();
            if (parent.generation < parent.maxGeneration)
        {
            for (int i = 0; i < parent.numberOfChildren; i++)
            {
                GameObject childGameObject = Instantiate(parent.SplitterPrefab, transform.position + new Vector3(Random.Range(-2.25f, 2.25f), Random.Range(-2.25f, 2.25f), Random.Range(-2.25f, 2.25f)), Quaternion.identity);
                EnemyTypeSplitter child = childGameObject.GetComponent<EnemyTypeSplitter>();
                Monster mc = childGameObject.GetComponent<Monster>();
                child.size = parent.size * parent.childrenSizeProportion;
                gameObject.transform.localScale = new Vector3(parent.size, parent.size, parent.size);
                child.generation = parent.generation += 1;
                mc.maxHealth = Mathf.FloorToInt(maxHealth * parent.childrenHealthProportion);
                mc.currHealth = mc.maxHealth;
                mc.speed = speed * parent.childrenSpeedProportion;
            }

        }
        }
        Destroy(gameObject,.15f);
    }

    IEnumerator DoT(){
        for (int i = 0; i < 6; i++) 
        {
            //Tickrate
            yield return new WaitForSeconds(1);
            currHealth -= 1;
            hb.TakeDamage(1);
        }

    }
}