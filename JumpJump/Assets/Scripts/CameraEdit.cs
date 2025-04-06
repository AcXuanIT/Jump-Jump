using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraEdit : MonoBehaviour
{
    private void Update()
    {
        AdjustCamera();
    }
    void AdjustCamera()
    {
        Camera cam = Camera.main;

        float targetAspect = 1080f / 1920f; // thiết kế gốc của bạn là 1080x1920 (tỷ lệ 9:16)
        float windowAspect = (float)Screen.width / (float)Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            // màn hình cao hơn thiết kế: thêm letterbox (viền đen 2 bên)
            Rect rect = cam.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2.0f;

            cam.rect = rect;
        }
        else
        {
            // màn hình rộng hơn thiết kế: thêm pillarbox (viền đen trên dưới)
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = cam.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            cam.rect = rect;
        }
    }
}
