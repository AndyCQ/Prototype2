using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreenManager : MonoBehaviour
{
    public bool AutoStart = true;
    public float TimeBeforeSkip;

    // onFinish
    public UnityEvent onFinish = new UnityEvent();
    // Scene to load
    [SerializeField] public string SceneToLoad;
    [Tooltip("Can be a GameObject instance or prefab")] public GameObject LoadingObject;
    // Fade
    public bool FadeOutAtEnd;
    public float FadeOutDuration = 1;

    public bool Viewing { private set; get; }
    public AsyncOperation Async { private set; get; } // Public if you want your script (like the load GameObject) take async.progress
    public Image Image { private set { _Image = value; } get { if (_Image == null) _Image = GetComponent<Image>(); return _Image; } }
    private Image _Image;
    private GameObject Instantiate_gameObject;
    public static Texture2D lastScreen;

    void Start()
    {
        if (AutoStart)
        {
            Views();
            StartLoad();
        }
    }

    private void StartLoad()
    {
        if (string.IsNullOrEmpty(SceneToLoad))
            return;

        Async = SceneManager.LoadSceneAsync(SceneToLoad);
        if (Async == null)
            return;
        Async.allowSceneActivation = false;

        if (!LoadingObject) // Do nothings if don't exist
            return;

        if (Async.progress <= 0.9)
            Loading(false);
        else
        {
#if UNITY_2017_2_OR_NEWER
            Async.completed += (a) => { Loading(false); };
#else
            StartCoroutine(AsyncCompleted());
#endif
            // Loading(true);
        }
    }


#if !UNITY_2017_2_OR_NEWER
    IEnumerator AsyncCompleted()
    {
        while (!Async.isDone)
            yield return new WaitForEndOfFrame();

        Loading(false);
    }
#endif

    private void StartScene()
    {
        if (Async == null)
            return;

        StartCoroutine(TakeSnapshot());

        Async.allowSceneActivation = true;
    }

    private IEnumerator TakeSnapshot()
    {
        yield return new WaitForEndOfFrame();

        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, true);
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();

        lastScreen = texture;
    }

    private void Loading(bool Active)
    {
        if (!LoadingObject)
            return;

        if (Active)
        {
            if (LoadingObject.scene.rootCount == 0) // if prefabs => Instantiate
            {
                GameObject gameObject;
                gameObject = Instantiate(LoadingObject);
                Instantiate_gameObject = gameObject;
                Instantiate_gameObject.transform.SetParent(transform.parent);
            }
            if (Instantiate_gameObject)
                Instantiate_gameObject.SetActive(true);
            else
                LoadingObject.SetActive(true);
        }
        else
        {
            if (LoadingObject.scene.rootCount == 0)
            {
                if (Instantiate_gameObject)
                {
                    if (Application.isPlaying)
                        Destroy(Instantiate_gameObject);
                    else
                        DestroyImmediate(Instantiate_gameObject);
                }
            }
            else
                LoadingObject.SetActive(false);
        }
    }

    async public void Views(bool preview = false)
    {
#if !UNITY_2018_1_OR_NEWER
        if (preview && !Application.isPlaying)
        {
            Debug.LogWarning("Preview mode only works on Unity 2018 or newer");
            return;
        }
#endif
        if (preview)
            Loading(true);

        Viewing = true;

        foreach (Transform child in transform)
            child.gameObject.SetActive(false);

        foreach (Transform child in gameObject.transform)
        {
            if (preview)
                Debug.Log("Viewing: " + child.name);
            await child.GetComponent<SplashScreen>().View(preview);
        }

        Viewing = false;
        if (preview)
        {
            Debug.Log("Viewing done!");
        }
        else
        {
            if (!Application.isPlaying)
                return;
            onFinish.Invoke();
            StartScene();
        }

        Loading(false);
    }
}