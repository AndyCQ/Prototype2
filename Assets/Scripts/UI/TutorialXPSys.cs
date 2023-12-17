using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class TutorialXpSys : MonoBehaviour
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

    public Button shield;
    public Button knockback;
    public Button PDs;
    public Button DJ;
    public TMP_Text supportCost;
    public TMP_Text SFCost;
    public TMP_Text MCost;

    public GameObject UI;
    public GameObject supportUI;
    public GameObject combatUI;
    public GameObject mobilityUI;
    public GameObject SheildCoolDown;
    public GameObject KnockBackCoolDown;
    public GameObject PoisonDartsCoolDown;

    void Awake() {
        if(SceneManager.GetActiveScene().name == "TutorialVer2"){
            PublicVars.starting_health = 1;
        }
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCode>();
        PublicVars.total_xp = PublicVars.starting_xp;
        PublicVars.atk_cost = PublicVars.starting_atk_cost;
        PublicVars.atkSpd_cost = PublicVars.starting_atkSpd_cost;
        PublicVars.jmp_cost = PublicVars.starting_jmp_cost;
        PublicVars.spd_cost = PublicVars.starting_jmp_cost;
        PublicVars.health_cost = PublicVars.starting_health_cost;
        
        PublicVars.support = PublicVars.currSA;
        PublicVars.secondaryFire = PublicVars.currSF;
        PublicVars.mobility = PublicVars.currM;

        PublicVars.atkLvl = PublicVars.s_atkLvl;
        PublicVars.atkSpd = PublicVars.s_atkSpd;
        PublicVars.spd = PublicVars.s_spd;
        PublicVars.jmp = PublicVars.s_jmp;
        PublicVars.health = PublicVars.starting_health;
        
    }

    // Update is called once per frame
    void Update()
    {
        atkSpd.text = "AtkSpd Level: "+ PublicVars.atkSpd;
        spd.text = "Speed Level: "+ PublicVars.spd;
        jmp.text = "Jump Level: "+ PublicVars.jmp;
        health.text = "Health Boost: "+ PublicVars.health;

        atkSpd_cost.text = PublicVars.atkSpd_cost.ToString();
        atkPwr_cost.text = PublicVars.atk_cost.ToString();
        spd_cost.text = PublicVars.spd_cost.ToString();
        jmp_cost.text = PublicVars.jmp_cost.ToString();
        health_cost.text = PublicVars.health_cost.ToString();

        total_xp.text = "Total XP: " + PublicVars.total_xp.ToString();

    

        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Escape)){
            OpenCloseMenu();
        }
        if(PublicVars.support == "Shield" || PublicVars.support == "Knockback"){
            supportUI.SetActive(false);

            if(PublicVars.support == "Shield"){
                SheildCoolDown.SetActive(true);
            }
            else KnockBackCoolDown.SetActive(true);
            }
        // if(PublicVars.support == "knockback"){
        //     shield.gameObject.SetActive(false);
        //     supportCost.gameObject.SetActive(false);
        //     }
        if(PublicVars.secondaryFire == "PDs"){
            combatUI.SetActive(false);
            if(!PoisonDartsCoolDown.activeSelf){
                PoisonDartsCoolDown.SetActive(true);
            }
            }
        if(PublicVars.mobility == "DJ"){
            mobilityUI.SetActive(false);
            }
        
    }

    public void OpenCloseMenu(){
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
    public void incAtkSpd(){
        if(PublicVars.total_xp >= PublicVars.atkSpd_cost && player.bulletBoost_count < 2){
            player.atkCD_Timer -= atkSpdIncr;
            player.bulletBoost_count += 1;
            PublicVars.total_xp -= PublicVars.atkSpd_cost;
            PublicVars.atkSpd_cost+=5;
        }
    }


    public void incAtkPwr(){
        if(PublicVars.total_xp >= PublicVars.atk_cost && pwrLevel < 2){
            PublicVars.bulletDMG += 1;
            pwrLevel += 1;
            PublicVars.total_xp -= PublicVars.atk_cost;
            PublicVars.atk_cost +=5;
        }
    }


    public void incSpd(){
        if(PublicVars.total_xp >= PublicVars.spd_cost && player.speepBoost_count < 2){
            player.speed += speedIncr;
            player.speepBoost_count += 1;
            PublicVars.total_xp -= PublicVars.spd_cost;
            PublicVars.spd_cost +=5;
        }
    }


    public void incJmp(){
        if(PublicVars.total_xp >= PublicVars.jmp_cost && player.jumpBoost_count < 2){
            player.jumpForce += jumpForceIncr;
            player.jumpBoost_count += 1;
            PublicVars.total_xp -= PublicVars.jmp_cost;
            PublicVars.jmp_cost +=5;
        }
    }


    public void incHealth(){
        if(PublicVars.total_xp >= PublicVars.health_cost && player.maxHealth < 2){
            healtBoost += 1;
            player.maxHealth += 1;
            PublicVars.total_xp -= PublicVars.health_cost;
            PublicVars.health_cost +=5;
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
            PublicVars.total_xp -= PublicVars.combatCost;
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
