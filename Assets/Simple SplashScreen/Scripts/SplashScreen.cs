using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class SplashScreen : MonoBehaviour
{
    private SplashScreenManager SplashScreenManager;
    private SemaphoreSlim signal;
    private CancellationTokenSource tokenSource;
    private CancellationToken CancellationToken;
    private bool CanSkip = true;
    private bool SkipRequest;

    public SplashScreenMode splashScreenMode;
    [HideInInspector] public float SplashScreenDuration;
    public Animator Animator { private set; get; }

    public bool ShowCursor = false;
    public bool Viewing { private set; get; }
    public bool Private { private set; get; }

    public enum SplashScreenMode
    {
        WaitUntilTheEndOfTheDuration,
        WaitUntilAnimationFinishes,
        WaitForASignal,
    }

    private void Awake()
    {
        tokenSource = new CancellationTokenSource();
        CancellationToken = tokenSource.Token;

        if (splashScreenMode == SplashScreenMode.WaitUntilAnimationFinishes)
            Animator = GetComponent<Animator>();

        if (!SplashScreenManager)
            SplashScreenManager = transform.parent.GetComponent<SplashScreenManager>();
    }

    async public Task View(bool preview = false)
    {
#if !UNITY_2018_1_OR_NEWER
        if (preview && !Application.isPlaying)
        {
            Debug.LogWarning("Preview mode only works on Unity 2018 or newer");
            return;
        }
#endif

        SkipRequest = false;
        Viewing = true;
        gameObject.SetActive(true);
        if (Application.isPlaying)
            Cursor.visible = ShowCursor;

        StartCoroutine(CanSkipAfter(SplashScreenManager.TimeBeforeSkip));

        switch (splashScreenMode)
        {
            case SplashScreenMode.WaitUntilTheEndOfTheDuration:
                await Delay(SplashScreenDuration);
                break;

            case SplashScreenMode.WaitUntilAnimationFinishes:
                if (!Animator)
                    Animator = GetComponent<Animator>();

#if UNITY_EDITOR
                if (!Application.isPlaying)
                {   // We have to do a clone because this method cannot reset the GameObject a the end without any undo possible
                    Debug.LogWarning("The animation preview when the editor is not currently playing is inaccurate!");
                    gameObject.SetActive(false);
                    GameObject PreviewObject;
                    PreviewObject = Instantiate(this.gameObject, transform.parent);
                    PreviewObject.SetActive(true);

                    Animator PreviewAnimator = PreviewObject.GetComponent<Animator>();
                    PreviewObject.GetComponent<SplashScreen>().Private = true;

                    EditorApplication.CallbackFunction action =
                        new EditorApplication.CallbackFunction(() => { PreviewAnimator.Update(Time.deltaTime / 1.5f); });
                    EditorApplication.update += action;

                    await Delay(Animator.runtimeAnimatorController.animationClips[0].length);

                    EditorApplication.update -= action;
                    DestroyImmediate(PreviewAnimator.gameObject);
                }
                else
#endif
                    await Delay(Animator.runtimeAnimatorController.animationClips[0].length);

                break;

            case SplashScreenMode.WaitForASignal:
                if (!Application.isPlaying)
                    await Delay(1);
                else
                {
                    signal = new SemaphoreSlim(0, 1);
                    try
                    {
                        await signal.WaitAsync(CancellationToken);
                    } catch (System.OperationCanceledException)
                    {
                        return;
                    }
                    signal.Dispose();
                }
                break;
        }
        Viewing = false;

        if (transform.GetSiblingIndex() + 1 != transform.parent.childCount)
            gameObject.SetActive(false);
    }

    public void Done()
    {
        if (splashScreenMode == SplashScreenMode.WaitForASignal)
            signal.Release();
    }

    private void Skip()
    {
        // if (!CanSkip)
            // return;

        // if (splashScreenMode == SplashScreenMode.WaitForASignal)
            // return;

        SkipRequest = true;
        CanSkip = false;
    }

    public float GetDuration()
    {
        switch (splashScreenMode)
        {
            case SplashScreenMode.WaitUntilTheEndOfTheDuration:
                return SplashScreenDuration;

            case SplashScreenMode.WaitUntilAnimationFinishes:
                if (!Animator)
                    Animator = GetComponent<Animator>();
                return Animator.runtimeAnimatorController.animationClips[0].length;

            case SplashScreenMode.WaitForASignal:
                return 0;

            default:
                return 0;
        }
    }

    private async Task Delay(float secondsDelay)
    {
        float start = Time.realtimeSinceStartup;
        int steps = 100;
        int interval = (int)(secondsDelay * 1000) / steps;
        for (int i = 0; i < interval; i++)
        {
            await Task.Delay(steps, CancellationToken);
            if (SkipRequest)
            {
                print("set to false");
                SkipRequest = false;
                return;
            }
            // It's inaccurate, so I add that
            if (secondsDelay < Time.realtimeSinceStartup - start)
                break;
        }
    }

    private void OnDestroy()
    {
        if (tokenSource != null)
            tokenSource.Cancel();
    }

    void Update()
    {
        if (Input.anyKeyDown)
        {
            GameObject nextChild = transform.parent.GetChild(transform.GetSiblingIndex() + 1).gameObject;
            nextChild.SetActive(true);
            gameObject.SetActive(false);
            // Skip();
        }
    }

    private IEnumerator CanSkipAfter(float secondes)
    {
        CanSkip = false;
        yield return new WaitForSeconds(secondes);
        CanSkip = true;
    }
}