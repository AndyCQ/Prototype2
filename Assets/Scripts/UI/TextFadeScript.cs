using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextFadeScript : MonoBehaviour
{
    public float fadeDuration = 2.0f; // Duration of the fade in seconds
    private Text textToFade;

    void OnEnable()
    {
        // Ensure Text component is assigned
        textToFade = GetComponent<Text>();
        if (textToFade == null)
        {
            Debug.LogError("Text component not assigned!");
            return;
        }

        // Ensure the text is initially transparent
        textToFade.color = new Color(1, 1, 1, 0);

        // Start the fade-in process
        StartCoroutine(FadeInText());
    }

    IEnumerator FadeInText()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            textToFade.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the text is fully visible at the end
        textToFade.color = new Color(1, 1, 1, 1);
    }
}
