using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starttimer : MonoBehaviour
{
    public GameObject timer;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            timer.SetActive(true);
        }
    }
}
