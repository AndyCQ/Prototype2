using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpToMain : MonoBehaviour
{
    public void LoadLevel() {
        PublicVars.score = 0;
        SceneManager.LoadScene(0);
    }
}
