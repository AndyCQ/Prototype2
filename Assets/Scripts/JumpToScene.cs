using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpToScene : MonoBehaviour
{
    AudioSource clickSFX;
    
    void Start()
    {
        AudioSource[] clickSFXList = gameObject.GetComponents<AudioSource>();
        if (clickSFXList.Length != 0) clickSFX = clickSFXList[0];
    }

    public void Play(){
        if (clickSFX != null) clickSFX.Play();
        Invoke("LoadLevel", 0.2f);
    }

    public void LoadLevel() {
        PublicVars.score = 0;
        SceneManager.LoadScene(PublicVars.currLevel);
    }

    public void PlayTutorial(){
        clickSFX.Play();
        Invoke("LoadTutorial", 0.2f);
    }

    public void LoadTutorial() {
        PublicVars.score = 0;
        SceneManager.LoadScene("TutorialVer2");
    }

    public void LoadIntroScene()
    {
        SceneManager.LoadScene("IntroScene");
    }
}
