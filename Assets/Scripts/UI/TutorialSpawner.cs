using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpawner : MonoBehaviour
{
    public GameObject enemy;

    void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "Player"){
            GameObject tmp;
            tmp = Instantiate(enemy, new Vector2(transform.position.x + 10,transform.position.y), Quaternion.identity);
            tmp.transform.localScale *= new Vector2(-1,1);
            Instantiate(enemy, new Vector2(transform.position.x - 10,transform.position.y), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
