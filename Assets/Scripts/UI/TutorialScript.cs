using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialScript : MonoBehaviour
{
    public GameObject abilities;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            abilities.SetActive(true);
            PublicVars.total_xp += 200;
        }
    }
}
