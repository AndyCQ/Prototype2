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
                Debug.Log("timer reached zero");
                //add migrate to end scene here after complete
                StartTimer = false;
                currentTime = 0;
            }
        }
        TimerText.text = currentTime.ToString("f2");
    }

}
