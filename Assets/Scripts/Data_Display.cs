using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Data_Display : MonoBehaviour
{
    [SerializeField] Text DataText;
    GameObject obj;

    void Awake()
    {
        obj = GameObject.FindGameObjectWithTag ("Player");
    }


    void Update()
    {
        DataText.text = "Speed Level " + obj.GetComponent<PlayerCode>().speepBoost_count + "\n" +
            "Jump Level " + obj.GetComponent<PlayerCode>().jumpBoost_count + "\n" + 
            "Bullet Speed Level " + obj.GetComponent<PlayerCode>().bulletBoost_count + "\n" +
            "Score :  " + obj.GetComponent<PlayerCode>().sum_count;
    }
}
