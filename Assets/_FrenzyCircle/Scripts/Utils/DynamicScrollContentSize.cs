using UnityEngine;
using UnityEngine.UI;


namespace FrenzyCircle
{
    public class DynamicScrollContentSize : MonoBehaviour
    {
        private RectTransform _rectTr;
        public RankingPanel basePanel;

        private void Awake()
        {
            _rectTr = GetComponent<RectTransform>();
        }

        private void FixedUpdate()
        {
            if (transform.childCount > 0)
            {
                var height = transform.GetChild(0).GetComponent<LayoutElement>().minHeight;
                _rectTr.sizeDelta = new Vector2(0, height * transform.childCount + height);
            }
        }
    }
}
