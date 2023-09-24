using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Zoom : MonoBehaviour
{
    private Camera cam;
    private float targetZoom;
    //private float zoomFactor = 3f;
    private float zoomLerpSpeed = 10;
    bool _StartTimer = false;
    private float _currentTime = 0;
    private float _targetTime = 3f;
    bool OneTime = false;
    private float tempZoom;
    [SerializeField] public float OffsetValue;

    void Start()
    {
        cam = Camera.main;
        targetZoom = cam.orthographicSize;
        tempZoom = targetZoom + OffsetValue;
    }

    void Update()
    {
        if (!OneTime){
            if (_StartTimer){
                _currentTime -= Time.deltaTime;
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomLerpSpeed);
                if (_currentTime <= 0)
                {
                    _StartTimer = false;
                    _currentTime = 0;
                    OneTime = true;
                }
            } else {
                _currentTime += Time.deltaTime;
                cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, tempZoom, Time.deltaTime * zoomLerpSpeed);
                if (_currentTime >= _targetTime){
                    _StartTimer = true;
                }
            }
        }
    }

}
