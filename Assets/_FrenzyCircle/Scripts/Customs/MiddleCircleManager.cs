using UnityEngine;
using System.Collections;


namespace FrenzyCircle
{
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

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(.1f);
            _renderer.sprite = ThemeManager.Instance.GetMiddleCircleSprite();
            _animator.SetInteger(Constant.Theme, ES3.Load(Constant.Theme, 0));
        }
    }
}
