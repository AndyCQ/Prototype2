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
        SceneManager.LoadScene("EndGame");
    }

}
