using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    float moveSpeed = .1f;
    int bulletForce = 500;
    public float distance = 10;

    public int currHealth;
    private int maxHealth = 50;

    public GameObject bulletPrefab1;

    GameObject player;
    Vector2 startingPos;
    Rigidbody2D _rigidbody;
    PlayerCode playercode;

    public Transform firePoint;

    public Transform[] positions;

    //Aimed shot atk
    int aimedShots = 6;
    float aimSpeed = .5f;
    float reload = .5f;
    float delay = .5f;
    public GameObject StunBullet;

    //Spawner atk
    public GameObject[] enemies;
    public Transform[] spawnPoints;
    public int minionHP;

    //Thunder atk
    public Transform[] clouds;
    int boltSpeed = 400;
    public GameObject thunderbolt;
    public int NumOfBolts = 2;

    //How long the boss stays in place after an atk
    float atkCD = 5;
    
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        playercode = player.GetComponent<PlayerCode>();
        NextAttack();
        startingPos = transform.position;
        _rigidbody = GetComponent<Rigidbody2D>();
        currHealth = maxHealth;
    }

    void NextAttack(){
        StopAllCoroutines();
        //int state = Random.Range(0,4);
        int state = 0;
        switch(state)
        {
            case 0:
                StartCoroutine(AimAttack());
                break;
            case 1:
                StartCoroutine(Spawner());
                break;
            case 2:
                StartCoroutine(BlindAttack());
                break;
            case 3:
                StartCoroutine(ThunderAttack());
                break;
            default:
                StartCoroutine(Idle());
                break;
        }

    }

    IEnumerator Idle(){
        yield return new WaitForSeconds(3);
        NextAttack();
    }

    IEnumerator AimAttack(){
        StartCoroutine(moveToPos(positions[1]));
        StartCoroutine(Fire(aimedShots, reload, delay));
        float t = 0;
        while(t < 2){
            firePoint.up = Vector3.Lerp(-firePoint.up, player.transform.position - firePoint.position, t);
            t += aimSpeed * Time.deltaTime;
            yield return null;
        }
        StartCoroutine(moveToPos(positions[0]));
        yield return new WaitForSeconds(atkCD);
        NextAttack();
    }

    IEnumerator Spawner(){
        StartCoroutine(moveToPos(positions[1]));
        yield return new WaitForSeconds(1);
        //Spawns snipers for the first 2 spawnpoints
        GameObject tmp;
        Monster mc;
        tmp = Instantiate(enemies[0], new Vector2(spawnPoints[0].position.x,spawnPoints[0].position.y), Quaternion.identity);
        mc = tmp.GetComponent<Monster>();

        tmp = Instantiate(enemies[0],new Vector2(spawnPoints[1].position.x,spawnPoints[1].position.y), Quaternion.identity);
        mc = tmp.GetComponent<Monster>();

        //Spawns random enemies at the other spawnpoints
        for (int i=2; i< spawnPoints.Length;i++){
            int type = Random.Range(0,6);
            tmp = Instantiate(enemies[type], new Vector2(spawnPoints[i].position.x,spawnPoints[i].position.y), Quaternion.identity);
            mc = tmp.GetComponent<Monster>();
            mc.maxHealth = minionHP;
            mc.currHealth = mc.maxHealth;
        }
        
        yield return new WaitForSeconds(10);
        StartCoroutine(moveToPos(positions[0]));
        yield return new WaitForSeconds(atkCD);
        NextAttack();
    }

    IEnumerator Fire(int shotNUm, float reload, float delay){
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < shotNUm; i++){
            GameObject newBullet = Instantiate(StunBullet, firePoint.position, transform.rotation * Quaternion.Euler(0,0,90));
            newBullet.GetComponent<Rigidbody2D>().AddForce(firePoint.up * bulletForce);
            yield return new WaitForSeconds(reload);
        }
    }

    IEnumerator ResetRotation(){
        float t = 0;
        while(t < 1){
            firePoint.up = Vector3.Lerp(firePoint.up, Vector2.up, t);
            t += Time.deltaTime;
            yield return null;
        }
        firePoint.rotation = Quaternion.identity;
    }

    IEnumerator moveToPos(Transform pos){
        float t = 0;
        while(t < 1){
            transform.position = Vector2.Lerp(transform.position, pos.transform.position, t*moveSpeed);
            t += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator BlindAttack(){
        StartCoroutine(moveToPos(positions[1]));
        yield return new WaitForSeconds(1);
        float t = 0;
        while (t < 30)
        {   
            yield return new WaitForSeconds(.1f);
            GameObject newBullet = Instantiate(bulletPrefab1, firePoint.position, transform.rotation * Quaternion.Euler(0,0,90));
            newBullet.GetComponent<Rigidbody2D>().AddForce(
                new Vector2((player.transform.position.x - firePoint.position.x),(player.transform.position.y - firePoint.position.y)).normalized * 
                bulletForce);   
            t += 1;
        }
        _rigidbody.velocity = new Vector2(0,0);
        StartCoroutine(moveToPos(positions[0]));
        yield return new WaitForSeconds(atkCD);
        NextAttack();
        }

    IEnumerator ThunderAttack(){
        StartCoroutine(moveToPos(positions[2]));
        yield return new WaitForSeconds(1.5f);
        GameObject bolt1;
        GameObject bolt2;
        int dist = 0;
        for(int i = 0; i<NumOfBolts;i+=2){
            bolt1 = Instantiate(thunderbolt,new Vector2(clouds[0].transform.position.x - dist, clouds[0].transform.position.y), transform.rotation * Quaternion.Euler(0,0,90));
            bolt1.GetComponent<Rigidbody2D>().AddForce(-transform.up * boltSpeed);
            bolt2 = Instantiate(thunderbolt,new Vector2(clouds[1].transform.position.x + dist, clouds[1].transform.position.y), transform.rotation * Quaternion.Euler(0,0,90));
            bolt2.GetComponent<Rigidbody2D>().AddForce(-transform.up * boltSpeed);
            dist += 5;
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(moveToPos(positions[0]));
        yield return new WaitForSeconds(atkCD);
        NextAttack();
    }

    
}
