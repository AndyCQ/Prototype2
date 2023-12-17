using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    [SerializeField] float startTime;
    public float currentTime;
    bool StartTimer = false;
    //referrence of Text component
    [SerializeField] Text TimerText;
    public Color og;
    public Color red;

    public GameObject player;

    void Start()
    {
        currentTime = startTime;
        TimerText.text = currentTime.ToString("f2");
        StartTimer = true;
        player = GameObject.FindGameObjectWithTag ("Player");
    }

    void Update()
    {
        if (StartTimer){
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                SceneManager.LoadScene("EndGame");
                //add migrate to end scene here after complete
                StartTimer = false;
                currentTime = 0;
            }
            if (currentTime <= 20){
                TimerText.color = red;
            }
            if( currentTime > 20){
                TimerText.color = og;
            }

        }

        TimerText.text = currentTime.ToString("f2");
    }

}
