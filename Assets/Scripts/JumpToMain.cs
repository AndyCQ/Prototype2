using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpToMain : MonoBehaviour
{
    AudioSource clickSFX;
    
    void Start()
    {
        clickSFX = gameObject.GetComponents<AudioSource>()[0];
    }

    public void Play(){
        clickSFX.Play();
        Invoke("LoadLevel", 0.5f);
    }

    

    public void LoadLevel() {
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

        PublicVars.supportCost = 35;
        PublicVars.combatCost = 60;
        PublicVars.mobilityCost = 35;

        PublicVars.currLevel = "Level1";

        PublicVars.totalTime = 0;
        
        SceneManager.LoadScene(0);
    }
}
