using UnityEngine;

public class Camera_Ratio : MonoBehaviour
{
    Camera mainCamera;

    private const float TargetWidth = 16.0f;
    private const float TargetHeight = 9.0f;

    // Start is called before the first frame update
    void Start()
    {
        this.mainCamera = GetComponent<Camera>();
        FitToScreen();
    }

    void FitToScreen()
    {
        float aspect = (float)Screen.width / (float)Screen.height;
        float targetAspect = TargetWidth / TargetHeight;

        float scaleHeight = aspect / targetAspect;

        if (scaleHeight < 1.0f)
        {
            Rect rect = this.mainCamera.rect;

            rect.width = 1.0f;
            rect.height = scaleHeight;
            rect.x = 0;
            rect.y = (1.0f - scaleHeight) / 2;

            this.mainCamera.rect = rect;
        }
        else
        {
            float scaleWidth = 1.0f / scaleHeight;

            Rect rect = this.mainCamera.rect;

            rect.width = scaleWidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scaleWidth) / 2.0f;
            rect.y = 0;

            this.mainCamera.rect = rect;
        }
    }
}
