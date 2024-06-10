using UnityEngine;

public class DynamicImageSize : MonoBehaviour
{
    public float width = 438.5f;
    public float height = 182f;
    public float scaleFactor = 0.75f;

    private RectTransform _rectTr;

    private void Awake()
    {
        _rectTr = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        ResizeTitleImage();
    }

    private void ResizeTitleImage()
    {
        // 현재 화면의 너비와 높이 가져오기
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // 주어진 비율에 따라 크기 조정
        float widthRatio = screenWidth / width;
        float heightRatio = screenHeight / height;
        float minRatio = Mathf.Min(widthRatio, heightRatio);

        // 최종 크기 계산
        float newWidth = width * scaleFactor;
        float newHeight = height * scaleFactor;

        // RectTransform의 sizeDelta 설정
        _rectTr.sizeDelta = new Vector2(newWidth, newHeight);

        // 상단에 위치하도록 앵커 및 피벗 설정
        _rectTr.anchorMin = new Vector2(0.5f, 1f);
        _rectTr.anchorMax = new Vector2(0.5f, 1f);
        _rectTr.pivot = new Vector2(0.5f, 1f);

        // 상단에서의 오프셋 설정
        _rectTr.anchoredPosition = new Vector2(0, -newHeight / 2);
    }
}