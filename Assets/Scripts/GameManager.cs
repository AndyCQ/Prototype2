using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameEnd = false;

    public void EndGame()
    {
        if (!gameEnd){
            gameEnd = true;
            Game_is_over();
        }
    }

    void Game_is_over()
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

        PublicVars.supportCost = 35;
        PublicVars.combatCost = 60;
        PublicVars.mobilityCost = 35;
        
        SceneManager.LoadScene("EndGame");
    }

}
