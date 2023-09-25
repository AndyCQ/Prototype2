using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpToScene : MonoBehaviour
{
    public void LoadLevel() {
        PublicVars.score = 0;
        SceneManager.LoadScene(1);
    }
}
