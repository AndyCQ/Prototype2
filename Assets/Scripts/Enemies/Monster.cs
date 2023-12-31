using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public int xpVal = 1;
    public bool monsterImmune = false;

    public string deathAudioClipPath;
    public string painAudioClipPath;
    private AudioSource _as;
    private AudioClip hurtSFX;
    private AudioClip deathSFX;
    public GameObject port;

    // public AudioSource voiceAudioSource;

    public bool isDead = false;

    void Start(){
        Mrb = GetComponent<Rigidbody2D>();
        speed = startSpd;
        currHealth = maxHealth;
        hb = GetComponent<HealthBar>();
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        _as = audioSource;
        hurtSFX = Resources.Load<AudioClip>(painAudioClipPath);
        deathSFX = Resources.Load<AudioClip>(deathAudioClipPath);
    }

//    void Update(){
//        if(currHealth <= 0){
//            Die();
//        }
//    }

    private void PlaySFX(AudioClip clip)
    {
        _as.PlayOneShot(clip);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bullet")){
            Destroy(other.gameObject);
            TakeDamage(PublicVars.bulletDMG);
        }
        if (other.CompareTag("PoisonDart")){
            Destroy(other.gameObject);
            StartCoroutine(DoT());
        }
        if (other.CompareTag("RailGunShot")){
            TakeDamage(PublicVars.RGDmg);
        }
    }

    public void TakeDamage(int damage)
    {
        if(gameObject.tag == "Enemy" || gameObject.tag == "Boss"){
            if (monsterImmune) return;
            if (currHealth <= 0) return;
            currHealth -= damage;
            hb.TakeDamage(damage);
            if (currHealth <= 0)
            {
                Die();
                PlaySFX(deathSFX);
            } else
                {
                    PlaySFX(hurtSFX);
                }
            }
    }

    public void Die() {
        if (isDead) return;
        isDead = true;
        PlaySFX(deathSFX);
        PublicVars.score += scoreVal;
        PublicVars.total_xp += xpVal;
        Debug.Log(xpVal);
        if(splitter){
            EnemyTypeSplitter parent = GetComponent<EnemyTypeSplitter>();
            if (parent.generation < parent.maxGeneration)
        {
            for (int i = 0; i < parent.numberOfChildren; i++)
            {
                GameObject childGameObject = Instantiate(parent.SplitterPrefab, transform.position + new Vector3(Random.Range(-2.25f, 2.25f), Random.Range(0, 2.25f), 0), Quaternion.identity);
                EnemyTypeSplitter child = childGameObject.GetComponent<EnemyTypeSplitter>();
                Monster mc = childGameObject.GetComponent<Monster>();
                child.size = parent.size * parent.childrenSizeProportion;
                gameObject.transform.localScale = new Vector3(parent.size, parent.size, parent.size);
                child.generation = parent.generation += 1;
                mc.maxHealth = Mathf.FloorToInt(maxHealth * parent.childrenHealthProportion);
                mc.currHealth = mc.maxHealth;
                mc.isDead = false;
                mc.speed = speed * parent.childrenSpeedProportion;
            }
        }
        }

        Transform deathPiecesTransform = gameObject.transform.Find("death_pieces");
        Transform gunTransform = gameObject.transform.Find("Gun");
        Transform healthBarTransform = gameObject.transform.Find("HealthBar");
        Transform firepointTransform = gameObject.transform.Find("Firepoint");
        Transform bowTransform = gameObject.transform.Find("Bow");
        if (deathPiecesTransform != null) deathPiecesTransform.gameObject.SetActive(true);
        if (gunTransform != null) gunTransform.gameObject.SetActive(false);
        if (healthBarTransform != null) healthBarTransform.gameObject.SetActive(false);
        if (firepointTransform != null) firepointTransform.gameObject.SetActive(false);
        if (bowTransform != null) bowTransform.gameObject.SetActive(false);
        gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;


        if(gameObject.tag == "Boss")
        {
            port.SetActive(true);
        }

        //if(gameObject.tag == "Boss"){
        //    PublicVars.starting_xp = 0;

        //    PublicVars.starting_atk_cost = 5;
        //    PublicVars.starting_atkSpd_cost = 5;
        //    PublicVars.starting_jmp_cost = 5;
        //    PublicVars.starting_spd_cost = 5;
        //    PublicVars.starting_health_cost = 5;

        //    PublicVars.currSA = "";
        //    PublicVars.currSF = "";
        //    PublicVars.currM = "";

        //    PublicVars.starting_bulletDMG = 2;
        //    PublicVars.starting_atk_spd = .7f;
        //    PublicVars.starting_jumpforce = 700;
        //    PublicVars.starting_health = 6;
        //    PublicVars.starting_spd = 10;

        //    PublicVars.s_atkLvl = 0;
        //    PublicVars.s_atkSpd = 0;
        //    PublicVars.s_spd = 0;
        //    PublicVars.s_jmp = 0;
        //    PublicVars.s_health = 0;

        //    port.SetActive(true);
        //    //SceneManager.LoadScene("WinGame");
        //}
        if (gameObject.tag != "Boss") Destroy(gameObject, 3f);
    }

    IEnumerator DoT(){
        for (int i = 0; i < 6; i++) 
        {
            ////Tickrate
            //currHealth -= 1;
            //if (currHealth <= 0)
            //{
            //    Die();
            //}
            // if (!isDead) hb.TakeDamage(1);
            TakeDamage(1);
            yield return new WaitForSeconds(.5f);
        }
    }
}