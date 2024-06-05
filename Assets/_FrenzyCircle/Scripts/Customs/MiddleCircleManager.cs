using UnityEngine;

public class MiddleCircleManager : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private Animator _animator;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _animator.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        _renderer.sprite = ThemeManager.Instance.GetMiddleCircleSprite();

        _animator.gameObject.SetActive(false);
        _animator.runtimeAnimatorController = ThemeManager.Instance.GetAnimatorController();
        _animator.Rebind();
        _animator.gameObject.SetActive(true);
    }
}