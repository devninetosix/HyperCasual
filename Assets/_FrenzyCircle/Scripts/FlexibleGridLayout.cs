﻿using UnityEngine;
using UnityEngine.UI;


namespace FrenzyCircle
{
    public class FlexibleGridLayout : MonoBehaviour
    {
        private void Start()
        {
            float width = gameObject.GetComponent<RectTransform>().rect.width;
            Vector2 newSize = gameObject.GetComponent<RectTransform>().rect.height > width
                ? new Vector2(width / 3 - 5, width / 3 - 5)
                : new Vector2(width / 3 - 120, width / 3 - 120);

            gameObject.GetComponent<GridLayoutGroup>().cellSize = newSize;
            GetComponent<RectTransform>().offsetMin = new Vector2(0, width - (width * 2.2f));
        }
    }
}
