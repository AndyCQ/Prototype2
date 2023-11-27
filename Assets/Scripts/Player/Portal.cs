using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private PlayerCode player;
    public int neededDucks = 3;
    public string nextLevel;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCode>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && player.ducksInLine == neededDucks){
            PublicVars.starting_xp = PublicVars.total_xp;

            PublicVars.starting_atk_cost = PublicVars.atk_cost;
            PublicVars.starting_atkSpd_cost = PublicVars.atkSpd_cost;
            PublicVars.starting_jmp_cost = PublicVars.jmp_cost;
            PublicVars.starting_jmp_cost = PublicVars.spd_cost;
            PublicVars.starting_health_cost = PublicVars.health_cost;

            PublicVars.currSA = PublicVars.support;
            PublicVars.currSF = PublicVars.secondaryFire;
            PublicVars.currM = PublicVars.mobility;

            PublicVars.starting_bulletDMG = PublicVars.bulletDMG;
            PublicVars.starting_atk_spd = player.atkCD_Timer;
            PublicVars.starting_jumpforce = player.jumpForce;
            PublicVars.starting_health = player.maxHealth;
            PublicVars.starting_spd = player.speed;

            PublicVars.s_atkLvl = PublicVars.atkLvl;
            PublicVars.s_atkSpd = PublicVars.atkSpd;
            PublicVars.s_spd = PublicVars.spd;
            PublicVars.s_jmp = PublicVars.jmp;
            PublicVars.s_health = PublicVars.health;
            

            SceneManager.LoadScene(nextLevel);
        }
    }
}
