using UnityEngine;
using UnityEngine.UI;

namespace FrenzyCircle
{
    public class SetBottomTheme : MonoBehaviour
    {
        private Image _img;

        private void Awake()
        {
            _img = GetComponent<Image>();
        }

        private void Start()
        {
            _img.sprite = ThemeManager.Instance.GetArrowSprite();
        }
    }
}