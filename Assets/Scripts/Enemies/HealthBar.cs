using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private float currHealth = 10f;


    //Healthbar vars
    private float maxHealth = 10f;
    public GameObject healthBarUI;
    public Slider slider;

    void Start()
    {
        maxHealth = GetComponent<Monster>().maxHealth;
        currHealth = maxHealth;
        slider.value = CalculateHealth();
    }


    // Update is called once per frame
    void Update()
    {
        // if (currHealth <= 0f)
        // {
        //     Destroy(gameObject);
        // }

        slider.value = CalculateHealth();

    }

    public void TakeDamage(float damage)
    {
        currHealth -= damage;
    }

    float CalculateHealth()
    {
        return currHealth / maxHealth;
    }

    public float GetHealth(){
        return currHealth;
    }
    
}
