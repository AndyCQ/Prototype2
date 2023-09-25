using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Over : MonoBehaviour
{
    public Text pointsText;


    void Update()
    {
        pointsText.text = PublicVars.score.ToString() + " POINTS";
    }
}
