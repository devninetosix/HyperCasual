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
        transform.Rotate(0, 0, rotateForward ? 1f : -1f * _rot * Time.deltaTime);

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
            if (Vars.StartGame)
            {
                gameObject.SetActive(false);
            }
            transform.localScale = new Vector2(0.75f, 0.75f);
            Vars.MainMenuCircles++;
            _sp.sortingOrder = -Vars.MainMenuCircles;
        }

        UpdateColor();
    }
    
    private void UpdateColor()
    {
        // 색상 조정
        Color c = Color.white;
        float normalizedScale = Mathf.InverseLerp(0.05f, 1.0f, transform.localScale.x);

        // 비선형 보간을 반대로 적용
        float colorFactor = 1.0f - normalizedScale + 0.05f;

        c.r = c.g = c.b = colorFactor;
        _sp.color = c;
    }
}