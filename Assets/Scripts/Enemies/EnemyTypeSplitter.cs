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

    Monster mc;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(MoveLoop());
        //spaceman = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCode>();
        gameObject.transform.localScale = new Vector3(size, size, size);
        mc = GetComponent<Monster>();
    }

    
    IEnumerator MoveLoop(){
        while (true)
        {
            
            yield return new WaitForSeconds(.1f);
            if(mc.moving){
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
                else if (Physics2D.Raycast(castPoint.position, transform.localScale, 2, GroundWallLayer)){
                    transform.localScale *= new Vector2(-1,1);
                }

                _rigidbody.velocity = new Vector2(mc.speed* transform.localScale.x, _rigidbody.velocity.y);
        }
        }
    }
  

}
