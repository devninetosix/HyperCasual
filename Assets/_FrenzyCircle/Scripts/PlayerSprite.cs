using UnityEngine;


namespace FrenzyCircle
{
    public class PlayerSprite : MonoBehaviour
    {
        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            _renderer.sprite = ThemeManager.Instance.GetArrowSprite();
        }
    }
}