using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEdit : MonoBehaviour
{
    public float targetWidth = 1080f; // Độ rộng mong muốn
    public float targetHeight = 1920f; // Độ cao mong muốn

    void Start()
    {
    
        float targetAspect = targetWidth / targetHeight;
        float screenAspect = (float)Screen.width / Screen.height;
        Camera.main.orthographicSize *= targetAspect / screenAspect;

        Screen.SetResolution((int)targetWidth, (int)targetHeight, false);
    }
}
