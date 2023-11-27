using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpToScene : MonoBehaviour
{
    AudioSource clickSFX;
    
    void Start()
    {
        clickSFX = gameObject.GetComponents<AudioSource>()[0];
    }

    public void Play(){
        clickSFX.Play();
        Invoke("LoadLevel", 0.8f);
    }

    public void LoadLevel() {
        PublicVars.score = 0;
        SceneManager.LoadScene("Level1_Remake");
    }

    public void PlayTutorial(){
        clickSFX.Play();
        Invoke("LoadTutorial", 0.8f);
    }

    public void LoadTutorial() {
        PublicVars.score = 0;
        SceneManager.LoadScene("Tutorial");
    }
}
