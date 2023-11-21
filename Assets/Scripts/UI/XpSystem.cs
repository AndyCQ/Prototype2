using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class XpSystem : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerCode player;

    public TMP_Text atkSpd;
    public TMP_Text atkPwr;
    public TMP_Text spd;
    public TMP_Text jmp;
    public TMP_Text health;

    public TMP_Text atkSpd_cost;
    public TMP_Text atkPwr_cost;
    public TMP_Text spd_cost;
    public TMP_Text jmp_cost;
    public TMP_Text health_cost;

    public TMP_Text total_xp;

    public float atkSpdIncr = .1f;
    public int jumpForceIncr = 100;
    public float speedIncr = 2;
    
    int pwrLevel = 0;
    int healtBoost = 0;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCode>();
        
    }

    // Update is called once per frame
    void Update()
    {
        atkSpd.text = "AtkSpd Level: "+ player.bulletBoost_count;
        atkPwr.text = "AtkPwr Level: "+ pwrLevel;
        spd.text = "Speed Level: "+ player.speepBoost_count;
        jmp.text = "Jump Level: "+ player.jumpBoost_count;
        health.text = "Health Boost: "+healtBoost;

        atkSpd_cost.text = PublicVars.atkSpd_cost.ToString();
        atkPwr_cost.text = PublicVars.atk_cost.ToString();
        spd_cost.text = PublicVars.spd_cost.ToString();
        jmp_cost.text = PublicVars.jmp_cost.ToString();
        health_cost.text = PublicVars.health_cost.ToString();

        total_xp.text = "Total XP: " + PublicVars.total_xp.ToString();
    }

    public void incAtkSpd(){
        if(PublicVars.total_xp >= PublicVars.atkSpd_cost){
            player.atkCD_Timer -= atkSpdIncr;
            player.bulletBoost_count += 1;
            PublicVars.total_xp -= PublicVars.atkSpd_cost;
            PublicVars.atkSpd_cost+=5;
        }
    }

    public void incAtkPwr(){
        if(PublicVars.total_xp >= PublicVars.atk_cost){
            PublicVars.bulletDMG += 1;
            pwrLevel += 1;
            PublicVars.total_xp -= PublicVars.atk_cost;
            PublicVars.atk_cost +=5;
        }
    }
    public void incSpd(){
        if(PublicVars.total_xp >= PublicVars.spd_cost){
            player.speed += speedIncr;
            player.speepBoost_count += 1;
            PublicVars.total_xp -= PublicVars.spd_cost;
            PublicVars.spd_cost +=5;
        }
    }
    public void incJmp(){
        if(PublicVars.total_xp >= PublicVars.jmp_cost){
            player.jumpForce += jumpForceIncr;
            player.jumpBoost_count += 1;
            PublicVars.total_xp -= PublicVars.jmp_cost;
            PublicVars.jmp_cost +=5;
        }
    }
    public void incHealth(){
        if(PublicVars.total_xp >= PublicVars.health_cost){
            healtBoost += 1;
            player.maxHealth += 1;
            PublicVars.total_xp -= PublicVars.health_cost;
            PublicVars.health_cost +=5;
        }
    }
}
