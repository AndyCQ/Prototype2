using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float startTime;
    public float currentTime;
    bool StartTimer = false;
    //referrence of Text component
    [SerializeField] Text TimerText;
    public Color og;
    public Color red;


    void Start()
    {
        currentTime = startTime;
        TimerText.text = currentTime.ToString("f2");
        StartTimer = true;
    }

    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartTimer = true;
        }
        */
        if (StartTimer){
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                FindObjectOfType<GameManager>().EndGame();

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
