using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    private Transform originalTransform;
    private Vector3 originalPosition;

    private void Start()
    {
        originalTransform = transform;
        originalPosition = originalTransform.localPosition;
    }

    public void Shake(float duration, float startIntensity, float endIntensity)
    {
        StartCoroutine(ShakeCoroutine(duration, startIntensity, endIntensity));
    }

    private IEnumerator ShakeCoroutine(float duration, float startIntensity, float endIntensity)
    {
        float elapsed = 0f;
        originalPosition = originalTransform.localPosition;

        while (elapsed < duration)
        {
            float normalizedTime = Mathf.Clamp01(elapsed / duration);
            float currentIntensity = Mathf.Lerp(startIntensity, endIntensity, normalizedTime);

            float x = originalTransform.localPosition.x + Random.Range(-1f, 1f) * currentIntensity;
            float y = originalTransform.localPosition.y + Random.Range(-1f, 1f) * currentIntensity;
            float z = originalTransform.localPosition.z;

            transform.localPosition = new Vector3(x, y, z);

            elapsed += Time.deltaTime;
            originalPosition = originalTransform.localPosition;

            yield return null;
        }

        // Reset the camera position after shaking
        transform.localPosition = originalPosition;
    }
}
