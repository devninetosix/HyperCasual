using UnityEngine;


namespace FrenzyCircle
{
    public class ObstacleStartSprite : MonoBehaviour
    {
        private SpriteRenderer _renderer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _renderer.sprite = ThemeManager.Instance.GetRotCircleSprite();
        }
    }
}