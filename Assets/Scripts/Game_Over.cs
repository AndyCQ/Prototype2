using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Over : MonoBehaviour
{
    public Text pointsText;


    void Update()
    {
        int display_score = PublicVars.score;
        pointsText.text = score.ToString() + " POINTS";
    }
}
