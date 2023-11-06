using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeSplitter : MonoBehaviour
{
    public float speed;
    public float distance = 4;
    public float lookDist = 15;

    public int jumpForce = 500;
    public bool grounded = false;

    public int currHealth;
    public int maxHealth = 3;

    public LayerMask GroundWallLayer;
    Rigidbody2D _rigidbody;
    Transform player;
    public Transform castPoint;
    public Transform feetTrans;

    public float knockbackPower = 4;
    private PlayerCode spaceman;

    // splitter specific.
    public GameObject SplitterPrefab;
    public float size;
    public float generation;
    public int numberOfChildren;
    public float maxGeneration;
    public float childrenSizeProportion;
    public float childrenSpeedProportion;
    public float childrenHealthProportion;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(MoveLoop());
        currHealth = maxHealth;
        //spaceman = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCode>();
        gameObject.transform.localScale = new Vector3(size, size, size);
    }
    
    IEnumerator MoveLoop(){
        while (true)
        {
            yield return new WaitForSeconds(.1f);
            if (Vector2.Distance(transform.position, player.position) < lookDist){
                if(player.position.x > transform.position.x && transform.localScale.x < 0 || 
                player.position.x < transform.position.x && transform.localScale.x > 0)
                {
                    transform.localScale *= new Vector2(-1,1);
                }
                // grounded = Physics2D.OverlapCircle(feetTrans.position, .3f, GroundWallLayer);
                // if(grounded){
                //     yield return new WaitForSeconds(1);
                //     
                // // }
                // yield return new WaitForSeconds(Random.Range(1,5));
                // _rigidbody.AddForce(new Vector2(0, jumpForce));
                  
            }
            //Platform version
            else if (Physics2D.Raycast(castPoint.position, transform.forward, 1, GroundWallLayer) ||
            !Physics2D.Raycast(castPoint.position,-transform.up,1,GroundWallLayer))
            {
                transform.localScale *= new Vector2(-1,1);
            }

            _rigidbody.velocity = new Vector2(speed* transform.localScale.x, _rigidbody.velocity.y);
        }
    }
  

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bullet")){
            Destroy(other.gameObject);
            currHealth -= PublicVars.bulletDMG;
        }
        if(currHealth <= 0){
                Die();
            }
    }

    void Die() {
        PublicVars.score += 3;

        if (generation < maxGeneration)
        {
            for (int i = 0; i < numberOfChildren; i++)
            {
                GameObject childGameObject = Instantiate(SplitterPrefab, transform.position + new Vector3(Random.Range(-2.25f, 2.25f), Random.Range(-2.25f, 2.25f), Random.Range(-2.25f, 2.25f)), Quaternion.identity);
                EnemyTypeSplitter child = childGameObject.GetComponent<EnemyTypeSplitter>();
                child.size = size * childrenSizeProportion;
                gameObject.transform.localScale = new Vector3(size, size, size);
                child.generation = generation += 1;
                child.maxHealth = Mathf.FloorToInt(maxHealth * childrenHealthProportion);
                child.currHealth = child.maxHealth;
                child.speed = speed * childrenSpeedProportion;
            }

        }

        Destroy(gameObject, .15f);
    }
}
