using System.Diagnostics;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class SplashScreenFadeOut : MonoBehaviour
{
    [Tooltip("In secondes")] public float duration = 0.5f;
    private RawImage rawImage;
    public bool DestroyWhenFinished = true;

    async void Start()
    {
        rawImage = GetComponent<RawImage>();
        if ((rawImage.texture = SplashScreenManager.lastScreen) != null)
        {
            await FadeOut();
            // If the scene is a menu, we don't want to see an old screenshot the second time.
            SplashScreenManager.lastScreen = null;
        }
        if (DestroyWhenFinished)
            Destroy(transform.root.gameObject);
    }

    async Task FadeOut()
    {
        ImageAlpha(1);
        float durationInMilis = duration * 1000;
        int steps = 100;
        int interval = (int)durationInMilis / steps;

        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        while (rawImage.color.a > 0) //Object is not fully invisible. Fade it in
        {
            await Task.Delay(interval);
            if (!Application.isPlaying)
                return;
            ImageAlpha(1 - ((float)stopWatch.Elapsed.TotalMilliseconds / durationInMilis));
        }

        stopWatch.Stop();
        ImageAlpha(0); //make fully visible
    }

    private void ImageAlpha(float a)
    {
        rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, a);
    }
}