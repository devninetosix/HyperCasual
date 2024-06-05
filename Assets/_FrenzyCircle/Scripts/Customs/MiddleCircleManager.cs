using UnityEngine;

public class MiddleCircleManager : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Animator _animator;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
    }

    private void Start()
    {
        _renderer.sprite = ThemeManager.Instance.GetMiddleCircleSprite();
        _animator.SetInteger("Theme", PlayerPrefs.GetInt("Theme", 0));
    }
}