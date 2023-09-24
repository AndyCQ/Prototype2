using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddTime : MonoBehaviour
{
    GameObject obj;

    void Awake()
    {
        obj = GameObject.FindGameObjectWithTag ("Timer");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            obj.GetComponent<Timer>().currentTime += 5;
        }
    }
}
