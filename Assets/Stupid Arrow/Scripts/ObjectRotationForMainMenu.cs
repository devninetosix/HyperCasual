using UnityEngine;

public class ObjectRotationForMainMenu : MonoBehaviour
{
    private float _rot;
    private float _startScale;
    public bool rotateForward = true;
    private SpriteRenderer _sp;
    private float _speed;

    private void Awake()
    {
        _startScale = transform.localScale.y;
        _rot = Random.Range(50, 100);
        _sp = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _speed = 0;
        transform.localScale = new Vector2(_startScale, _startScale);
    }

    private void FixedUpdate()
    {
        if (rotateForward)
        {
            transform.Rotate(0, 0, _rot * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0, 0, -_rot * Time.deltaTime);
        }

        if (Vars.StartGame)
        {
            _speed += 0.001f;
        }

        transform.localScale = new Vector2(
            transform.localScale.x - (0.0005f + _speed),
            transform.localScale.y - (0.0005f + _speed)
        );

        if (transform.localScale.x < 0f)
        {
            if (Vars.StartGame) Destroy(gameObject);
            transform.localScale = new Vector2(0.75f, 0.75f);
            Vars.MainMenuCircles++;
            _sp.sortingOrder = -Vars.MainMenuCircles;
        }

        // Color c = _sp.color;
        // c.r = Mathf.Clamp(132f / 255f - (1.2f - transform.localScale.x) * 0.1f, 0, 132f / 255f); // 초록 값 미세하게 감소
        // c.g = Mathf.Clamp(89f / 255f - (1.2f - transform.localScale.x) * 0.1f, 0, 89f / 255f); // 초록 값 미세하게 감소
        // c.b = Mathf.Clamp(232f / 255f - (1.2f - transform.localScale.x) * 0.1f, 0, 232f / 255f); // 파랑 값 미세하게 감소
        // _sp.color = c;
    }
}