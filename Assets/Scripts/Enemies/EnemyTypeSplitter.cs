using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTypeSplitter : MonoBehaviour
{
    public float distance = 4;
    public float lookDist = 15;

    public int jumpForce = 500;
    public bool grounded = false;


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
    public float spawnImmunity = 0.25f;
    public float collidableAfter = 0.5f;

    Monster mc;

    void Start()
    {
        mc = GetComponent<Monster>();
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(MoveLoop());
        //spaceman = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCode>();
        gameObject.transform.localScale = new Vector3(size, size, size);
        mc.monsterImmune = true;
        StartCoroutine(DisableImmunityAfterDelay(spawnImmunity));
    }

    private IEnumerator DisableImmunityAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        mc.monsterImmune = false;
        Debug.Log("Immunity disabled after " + delay + " seconds");
    }

    IEnumerator MoveLoop(){
        while (true)
        {
            
            yield return new WaitForSeconds(.1f);
            if(mc.moving && !mc.isDead)
            {
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
                else if (!Physics2D.Raycast(castPoint.position,-transform.up + transform.forward,3,GroundWallLayer))
                {
                    transform.localScale *= new Vector2(-1,1);
                }
                else if (Physics2D.Raycast(castPoint.position, transform.localScale, 3, GroundWallLayer)){
                    transform.localScale *= new Vector2(-1,1);
                }

                _rigidbody.velocity = new Vector2(mc.speed* transform.localScale.x, _rigidbody.velocity.y);
        }
        }
    }
  

}
