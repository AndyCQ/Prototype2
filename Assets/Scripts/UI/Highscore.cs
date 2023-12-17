using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{
    public Text totalTime;
    public Text highscoreText;
    public int highscore;
    void Awake(){
        highscore = PlayerPrefs.GetInt("highscore",0);
        totalTime.text = "Total Time: " + ((int)PublicVars.totalTime).ToString() + "s";
        if (highscore > (int)PublicVars.totalTime){
            PlayerPrefs.SetInt("highscore", (int)PublicVars.totalTime);
            }

        highscore = PlayerPrefs.GetInt("highscore",0);
        highscoreText.text = "Best Time: " + highscore.ToString() + "s";
        print(PublicVars.totalTime);
        print(highscore);
    }
    
}
