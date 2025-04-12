using UnityEngine;

public class CameraEdit : MonoBehaviour
{
    void Start()
    {
        int width = 1080;
        int height = 1920;
        bool fullscreen = true;

        Screen.SetResolution(width, height, fullscreen);
    }
}
