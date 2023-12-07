#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SplashScreen))]
[CanEditMultipleObjects]
public class SplashScreenEditor : Editor //When i'm to lazy to creat a second script
{
    public override void OnInspectorGUI()
    {
        var SplashScreen = target as SplashScreen;

        #region Tools
        GUILayout.BeginHorizontal();

        GUI.enabled = !SplashScreen.Viewing;
        if (GUILayout.Button("Preview"))
#if UNITY_2018_1_OR_NEWER
            _ = SplashScreen.View(true);
#else
#pragma warning disable CS4014 // Dans la mesure où cet appel n'est pas attendu, l'exécution de la méthode actuelle continue avant la fin de l'appel
            SplashScreen.View(true);
#pragma warning restore CS4014 // Dans la mesure où cet appel n'est pas attendu, l'exécution de la méthode actuelle continue avant la fin de l'appel
#endif
        GUI.enabled = true;

        if (!SplashScreen.gameObject.activeSelf)
            if (GUILayout.Button("Activate"))
                foreach (Transform child in SplashScreen.transform.parent)
                    child.gameObject.SetActive(SplashScreen.transform == child);

        if (GUILayout.Button("Back"))
        {
            SplashScreen.gameObject.SetActive(false);
            Selection.activeGameObject = SplashScreen.transform.parent.gameObject;
        }

        GUILayout.EndHorizontal();
#endregion

        base.OnInspectorGUI();

        serializedObject.Update();

        if (SplashScreen.splashScreenMode == SplashScreen.SplashScreenMode.WaitUntilTheEndOfTheDuration)
            serializedObject.FindProperty("SplashScreenDuration").floatValue = EditorGUILayout.FloatField("Splash Screen Duration:", SplashScreen.SplashScreenDuration);

        serializedObject.ApplyModifiedProperties();
    }
}
#endif