using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpToScene : MonoBehaviour
{
    public void LoadLevel() {
        SceneManager.LoadScene(1);
    }
}
