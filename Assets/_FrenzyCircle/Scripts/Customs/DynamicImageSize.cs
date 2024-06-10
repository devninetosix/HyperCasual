using UnityEngine;

public class DynamicImageSize : MonoBehaviour
{
    public float width = 438.5f;
    public float height = 182f;
    public float scaleFactor = 0.5f;

    private RectTransform _rectTr;

    private void Awake()
    {
        _rectTr = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        AdjustImageSize();
    }

    private void AdjustImageSize()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        float aspectRatio = width / height;

        float newWidth, newHeight;
        if (screenWidth / screenHeight > aspectRatio)
        {
            newHeight = screenHeight * scaleFactor;
            newWidth = newHeight * aspectRatio;
        }
        else
        {
            newWidth = screenWidth * scaleFactor;
            newHeight = newWidth / aspectRatio;
        }

        _rectTr.sizeDelta = new Vector2(newWidth, newHeight);

    }
}
