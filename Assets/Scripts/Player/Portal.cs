using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private PlayerCode player;
    public int neededDucks = 3;
    public string nextLevel;
    public List<GameObject> duckHolders = new List<GameObject>();
    public SpriteRenderer portalCover;
    float alphaLevel = 1.0f;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCode>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && player.ducksInLine == neededDucks)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                PublicVars.starting_xp = PublicVars.total_xp;

                PublicVars.starting_atk_cost = PublicVars.atk_cost;
                PublicVars.starting_atkSpd_cost = PublicVars.atkSpd_cost;
                PublicVars.starting_jmp_cost = PublicVars.jmp_cost;
                PublicVars.starting_spd_cost = PublicVars.spd_cost;
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

                PublicVars.supportCost = 35;
                PublicVars.combatCost = 60;
                PublicVars.mobilityCost = 35;

                if (nextLevel == "WelcomeScreen" || nextLevel == "WinGame")
                {
                    PublicVars.starting_xp = 0;

                    PublicVars.starting_atk_cost = 5;
                    PublicVars.starting_atkSpd_cost = 5;
                    PublicVars.starting_jmp_cost = 5;
                    PublicVars.starting_spd_cost = 5;
                    PublicVars.starting_health_cost = 5;

                    PublicVars.currSA = "";
                    PublicVars.currSF = "";
                    PublicVars.currM = "";

                    PublicVars.starting_bulletDMG = 2;
                    PublicVars.starting_atk_spd = .7f;
                    PublicVars.starting_jumpforce = 700;
                    PublicVars.starting_health = 6;
                    PublicVars.starting_spd = 10;

                    PublicVars.s_atkLvl = 0;
                    PublicVars.s_atkSpd = 0;
                    PublicVars.s_spd = 0;
                    PublicVars.s_jmp = 0;
                    PublicVars.s_health = 0;

                }
                SceneManager.LoadScene(nextLevel);
            }

            while (portalCover.color[3] > 0)
            {
                portalCover.color = new Color(portalCover.color[0], portalCover.color[1], portalCover.color[2], portalCover.color[3] - 0.01f);
            }
            player.ducksGoBack = true;

        }
    }
}
