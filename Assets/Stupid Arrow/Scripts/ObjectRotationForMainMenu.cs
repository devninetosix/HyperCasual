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

        Color c = _sp.color;
        c.g = 1.2f - transform.localScale.x;
        c.b = 1.2f - transform.localScale.x;
        _sp.color = c;
    }
}