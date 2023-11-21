using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health_Display : MonoBehaviour
{
    [SerializeField] Text healthText;
    GameObject obj;

    void Awake()
    {
        obj = GameObject.FindGameObjectWithTag ("Player");
    }


    void Update()
    {
        healthText.text = "HEALTH: " + obj.GetComponent<PlayerCode>().currHealth;
    }
}
