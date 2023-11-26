using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialScript : MonoBehaviour
{
    public TMP_Text cost;
    public Button button;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            if(cost){
                cost.gameObject.SetActive(true);
                PublicVars.total_xp += 200;
                }
            if(button){
                button.gameObject.SetActive(true);
            }
        }
    }
}
