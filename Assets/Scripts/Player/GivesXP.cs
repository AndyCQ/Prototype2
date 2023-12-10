using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GivesXP : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            PublicVars.total_xp += 999;
        }
    }
}
