using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : MonoBehaviour
{
    private void Start()
    {
        Vector2 newSize;
        float width = gameObject.GetComponent<RectTransform>().rect.width;

        newSize = gameObject.GetComponent<RectTransform>().rect.height > width
            ? new Vector2(width / 3 - 5, width / 3 - 5)
            : new Vector2(width / 3 - 120, width / 3 - 120);

        gameObject.GetComponent<GridLayoutGroup>().cellSize = newSize;
        GetComponent<RectTransform>().offsetMin = new Vector2(0, width - (width * 2.2f));
    }
}