using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyType1 : MonoBehaviour
{
    public float distance = 4;
    public float lookDist = 4;


    public LayerMask GroundWallLayer;
    Rigidbody2D _rigidbody;
    Transform player;
    public Transform castPoint;

    Monster mc;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(MoveLoop());
        mc = GetComponent<Monster>();
    }
    IEnumerator MoveLoop(){
        while (true)
        {
            yield return new WaitForSeconds(.1f);
            if(mc.moving){
                //Platform version
                if (!Physics2D.Raycast(castPoint.position,-transform.up + transform.forward,3,GroundWallLayer))
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
