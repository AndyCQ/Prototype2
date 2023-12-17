using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialTimer : MonoBehaviour
{
    GameObject obj;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            obj = GameObject.FindGameObjectWithTag ("Timer");
            obj.GetComponent<Timer>().currentTime += 10;
        }
    }
}
