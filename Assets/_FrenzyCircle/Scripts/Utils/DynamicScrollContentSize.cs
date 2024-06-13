using UnityEngine;
using UnityEngine.UI;

public class DynamicScrollContentSize : MonoBehaviour
{
    private RectTransform _rectTr;
    
    private void Awake()
    {
        _rectTr = GetComponent<RectTransform>();
    }
    
    private void FixedUpdate()
    {
        if (transform.childCount > 0)
        {
            var height = transform.GetChild(0).GetComponent<LayoutElement>().minHeight;
            _rectTr.sizeDelta = new Vector2(0, height * transform.childCount);
        }
    }
}
