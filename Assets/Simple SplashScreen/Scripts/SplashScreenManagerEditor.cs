#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

[CustomEditor(typeof(SplashScreenManager))]
public class SplashScreenManagerEditor : Editor
{
    private SplashScreenManager SplashScreenManager;
    private bool SplashScreenCurrentlyEdited = false;
    private bool InvokeFunction;
    private bool ActivateScene;

    public override void OnInspectorGUI()
    {
        SplashScreenManager = target as SplashScreenManager;
        float TotalDuration = 0;

        GUILayout.BeginVertical("box");

        GUILayout.Label("Manager:", EditorStyles.boldLabel);

        GUILayout.BeginVertical("box");

        if (SplashScreenManager.transform.childCount == 0)
            GUILayout.Label("Press \"Add a new Splash Screen\" button to add a new Splash Screen");

        foreach (Transform child in SplashScreenManager.transform)
        {
            SplashScreen splashScreen = child.GetComponent<SplashScreen>();

            if (splashScreen.Private)
                continue;

            TotalDuration += splashScreen.GetDuration();

            if (child.gameObject.activeSelf)
                SplashScreenCurrentlyEdited = true;

            GUILayout.BeginHorizontal();

            child.name = GUILayout.TextArea(child.name);

            if (splashScreen.splashScreenMode == SplashScreen.SplashScreenMode.WaitForASignal)
                GUILayout.Label("Trigger");
            else
                GUILayout.Label("Duration: " + splashScreen.GetDuration());

            if (GUILayout.Button("Edit"))
                Select(child);

            if (GUILayout.Button("Remove"))
            {
                Undo.DestroyObjectImmediate(child.gameObject);
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            }

            GUILayout.EndHorizontal();
        }

        GUILayout.EndVertical();

        GUILayout.Label("Total duration: " + TotalDuration + " secondes");

        #region Tools
        GUILayout.BeginHorizontal();

        // Add a new Splash Screen
        if (GUILayout.Button("Add a new Splash Screen"))
        {
            GameObject gameObject = new GameObject("New Splash Screen");
            Undo.RegisterCreatedObjectUndo(gameObject, "New Splash Screen");
            gameObject.SetActive(false);
            gameObject.transform.SetParent(SplashScreenManager.transform);
            gameObject.AddComponent<SplashScreen>();
            if (SplashScreenManager.GetComponent<Canvas>())
                gameObject.AddComponent<RectTransform>();
            EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
        }

        // Preview
        GUI.enabled = !SplashScreenManager.Viewing;
        if (GUILayout.Button("Preview"))
            SplashScreenManager.Views(true);
        GUI.enabled = true;

        // Stop editing
        if (SplashScreenCurrentlyEdited && !SplashScreenManager.Viewing)
            if (GUILayout.Button("Stop editing"))
            {
                SplashScreenCurrentlyEdited = false;
                foreach (Transform child in SplashScreenManager.transform)
                    child.gameObject.SetActive(false);
            }

        GUILayout.EndHorizontal();
        #endregion

        GUILayout.EndVertical();

        GUILayout.Space(5);

        #region serializedObject

        serializedObject.Update();

        GUILayout.Label("Settings:", EditorStyles.boldLabel);

        serializedObject.FindProperty("AutoStart").boolValue = EditorGUILayout.Toggle("Auto start", SplashScreenManager.AutoStart);
        serializedObject.FindProperty("TimeBeforeSkip").floatValue = EditorGUILayout.FloatField("Time before the user can skip", SplashScreenManager.TimeBeforeSkip);

        // onFinish
        if (InvokeFunction = EditorGUILayout.Foldout(InvokeFunction, "When it is over, invoke the function"))
            EditorGUILayout.PropertyField(serializedObject.FindProperty("onFinish"), true);

        // Scene to load
        if (ActivateScene = EditorGUILayout.Foldout(ActivateScene, "Scene to load"))
        {
            var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(SplashScreenManager.SceneToLoad);
            // Prevents the user from redefining the path when the path changes.
            if (oldScene == null)
                if (!string.IsNullOrEmpty(SplashScreenManager.SceneToLoad))
                {
                    var newPath = AssetDatabase.GetAssetPath(GetSceneObject(System.IO.Path.GetFileNameWithoutExtension(SplashScreenManager.SceneToLoad)));
                    var scenePathProperty = serializedObject.FindProperty("SceneToLoad");
                    scenePathProperty.stringValue = newPath;
                }

            EditorGUI.BeginChangeCheck();
            var newScene = EditorGUILayout.ObjectField("Scene to load", oldScene, typeof(SceneAsset), false) as SceneAsset;

            if (EditorGUI.EndChangeCheck())
            {
                var newPath = AssetDatabase.GetAssetPath(newScene);
                var scenePathProperty = serializedObject.FindProperty("SceneToLoad");
                scenePathProperty.stringValue = newPath;
            }

            serializedObject.FindProperty("LoadingObject").objectReferenceValue = (GameObject)EditorGUILayout.ObjectField("Loading GameObejct", SplashScreenManager.LoadingObject, typeof(GameObject), true);
        }

        serializedObject.ApplyModifiedProperties();

        #endregion
    }

    protected SceneAsset GetSceneObject(string sceneObjectName)
    {
        if (string.IsNullOrEmpty(sceneObjectName))
        {
            return null;
        }

        foreach (var editorScene in EditorBuildSettings.scenes)
        {
            if (editorScene.path.IndexOf(sceneObjectName) != -1)
            {
                return AssetDatabase.LoadAssetAtPath(editorScene.path, typeof(SceneAsset)) as SceneAsset;
            }
        }
        Debug.LogWarning("Scene [" + sceneObjectName + "] cannot be used. Add this scene to the 'Scenes in the Build' in build settings.");
        return null;
    }

    private void Select(Transform transform)
    {
        SplashScreenCurrentlyEdited = true;
        Selection.activeGameObject = transform.gameObject;
        foreach (Transform child in SplashScreenManager.transform)
        {
            child.gameObject.SetActive(child == transform);
        }
    }
}
#endif