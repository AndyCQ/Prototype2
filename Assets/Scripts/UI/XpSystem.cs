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
    public int jumpForceIncr = 50;
    public float speedIncr = 1.5f;
    
    public Button shield;
    public Button knockback;
    public Button PDs;
    public Button DJ;
    public TMP_Text supportCost;
    public TMP_Text SFCost;
    public TMP_Text MCost;

    public GameObject UI;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCode>();
        PublicVars.total_xp = PublicVars.starting_xp;
        
        PublicVars.atk_cost = PublicVars.starting_atk_cost;
        PublicVars.atkSpd_cost = PublicVars.starting_atkSpd_cost;
        PublicVars.jmp_cost = PublicVars.starting_jmp_cost;
        PublicVars.spd_cost = PublicVars.starting_spd_cost;
        PublicVars.health_cost = PublicVars.starting_health_cost;
        PublicVars.bulletDMG = PublicVars.starting_bulletDMG;

        PublicVars.support = PublicVars.currSA;
        PublicVars.secondaryFire = PublicVars.currSF;
        PublicVars.mobility = PublicVars.currM;

        PublicVars.atkLvl = PublicVars.s_atkLvl;
        PublicVars.atkSpd = PublicVars.s_atkSpd;
        PublicVars.spd = PublicVars.s_spd;
        PublicVars.jmp = PublicVars.s_jmp;
        PublicVars.health = PublicVars.s_health;
        
    }

    // Update is called once per frame
    void Update()
    {
        atkSpd.text = "AtkSpd Level: "+ PublicVars.atkSpd;
        atkPwr.text = "AtkPwr Level: "+ PublicVars.atkLvl;
        spd.text = "Speed Level: "+ PublicVars.spd;
        jmp.text = "Jump Level: "+ PublicVars.jmp;
        health.text = "Health Boost: "+ PublicVars.health;

        atkSpd_cost.text = PublicVars.atkSpd_cost.ToString();
        atkPwr_cost.text = PublicVars.atk_cost.ToString();
        spd_cost.text = PublicVars.spd_cost.ToString();
        jmp_cost.text = PublicVars.jmp_cost.ToString();
        health_cost.text = PublicVars.health_cost.ToString();

        total_xp.text = "Total XP: " + PublicVars.total_xp.ToString();

    

        if (Input.GetKeyDown(KeyCode.F)){
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCode>();
            if(UI.activeSelf){
                UI.SetActive(false);
                Time.timeScale = 1f;
                player.fireStatus = true;
            }
            else{
                UI.SetActive(true);
                Time.timeScale = 0f;
                player.fireStatus = false;
                }
        }
        if(PublicVars.support == "Shield"){
            knockback.gameObject.SetActive(false);
            supportCost.gameObject.SetActive(false);
            }
        if(PublicVars.support == "knockback"){
            shield.gameObject.SetActive(false);
            supportCost.gameObject.SetActive(false);
            }
        if(PublicVars.secondaryFire == "PDs"){
            //knockback.gameObject.SetActive(false);
            SFCost.gameObject.SetActive(false);
            }
        if(PublicVars.mobility == "DJ"){
            //knockback.gameObject.SetActive(false);
            MCost.gameObject.SetActive(false);
            }
    }


    public void incAtkSpd(){
        if(PublicVars.total_xp >= PublicVars.atkSpd_cost){
            player.atkCD_Timer -= atkSpdIncr;
            PublicVars.atkSpd += 1;
            PublicVars.total_xp -= PublicVars.atkSpd_cost;
            PublicVars.atkSpd_cost *= 2;
        }
    }


    public void incAtkPwr(){
        if(PublicVars.total_xp >= PublicVars.atk_cost){
            PublicVars.bulletDMG += 1;
            PublicVars.atkLvl += 1;
            PublicVars.total_xp -= PublicVars.atk_cost;
            PublicVars.atk_cost *= 2;
        }
    }


    public void incSpd(){
        if(PublicVars.total_xp >= PublicVars.spd_cost){
            player.speed += speedIncr;
            PublicVars.spd += 1;
            PublicVars.total_xp -= PublicVars.spd_cost;
            PublicVars.spd_cost *= 2;
        }
    }


    public void incJmp(){
        if(PublicVars.total_xp >= PublicVars.jmp_cost){
            player.jumpForce += jumpForceIncr;
            PublicVars.jmp += 1;
            PublicVars.total_xp -= PublicVars.jmp_cost;
            PublicVars.jmp_cost *= 2;
        }
    }


    public void incHealth(){
        if(PublicVars.total_xp >= PublicVars.health_cost){
            player.maxHealth += 1;
            PublicVars.health += 1;
            PublicVars.total_xp -= PublicVars.health_cost;
            PublicVars.health_cost *= 2;
        }
    }


    public void PurchaseShield(){
        if(PublicVars.total_xp >= PublicVars.supportCost && PublicVars.support == ""){
            PublicVars.support = "Shield";
            knockback.gameObject.SetActive(false);
            supportCost.gameObject.SetActive(false);
            PublicVars.total_xp -= PublicVars.supportCost;
        }
    }


    public void PurchaseForce(){
        if(PublicVars.total_xp >= PublicVars.supportCost && PublicVars.support == ""){
            PublicVars.support = "Knockback";
            knockback.gameObject.SetActive(false);
            supportCost.gameObject.SetActive(false);
            PublicVars.total_xp -= PublicVars.supportCost;
        }
    }

    public void PurchasePDs(){
        if(PublicVars.total_xp >= PublicVars.combatCost && PublicVars.secondaryFire == ""){
            PublicVars.secondaryFire = "PDs";
            SFCost.gameObject.SetActive(false);
            PublicVars.total_xp -= PublicVars.supportCost;
        }
    }

    public void PurchaseDJ(){
        if(PublicVars.total_xp >= PublicVars.mobilityCost && PublicVars.mobility == ""){
            PublicVars.mobility = "DJ";
            MCost.gameObject.SetActive(false);
            PublicVars.total_xp -= PublicVars.mobilityCost;
        }
    }
}
