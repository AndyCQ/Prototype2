using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : MonoBehaviour
{
    public float scaleSpeed = 1.0f;
    public Vector3 targetScale = new Vector3(10.0f, 10.0f, 10.0f);

    private void Start()
    {
        // Optionally, you can initialize the object's scale to a starting value
        // transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        // Start the scaling coroutine
        StartCoroutine(ScaleObject());
    }

    private IEnumerator ScaleObject()
    {
        float t = 0.0f;
        Vector3 initialScale = transform.localScale;

        while (t <1f)
        {
            t += Time.deltaTime * scaleSpeed;
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            yield return null;
        }

        // Ensure the object reaches the exact target scale
        transform.localScale = targetScale;

    }
}
