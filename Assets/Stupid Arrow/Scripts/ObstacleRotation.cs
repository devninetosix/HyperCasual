using UnityEngine;

public class ObstacleRotation : MonoBehaviour
{
    public float rot;
    public bool rotateForward = true;
    private float _timer;
    private SpriteRenderer _sp;

    private void Start()
    {
        rot = Random.Range(50, 100);
        _sp = GetComponent<SpriteRenderer>();
        // Color c = new Color(132f / 255f, 89f / 255f, 232f / 255f); // 초기 보라색 설정
        // _sp.color = c;
    }

    private void FixedUpdate()
    {
        if (rotateForward)
        {
            transform.Rotate(0, 0, rot * Time.deltaTime);
        }
        else
        {
            transform.Rotate(0, 0, -rot * Time.deltaTime);
        }

        _timer += Time.deltaTime;
        if (_timer > 5f)
        {
            _timer = 0;
            Vars.ObstacleScaleSpeed += 0.00001f;
        }

        transform.localScale = new Vector2(transform.localScale.x - (0.0005f + Vars.ObstacleScaleSpeed),
            transform.localScale.y - (0.0005f + Vars.ObstacleScaleSpeed));

  
        // Color c = _sp.color;
        // c.r = Mathf.Clamp(132f / 255f - (1.2f - transform.localScale.x) * 0.1f, 0, 132f / 255f); // 초록 값 미세하게 감소
        // c.g = Mathf.Clamp(89f / 255f - (1.2f - transform.localScale.x) * 0.1f, 0, 89f / 255f); // 초록 값 미세하게 감소
        // c.b = Mathf.Clamp(232f / 255f - (1.2f - transform.localScale.x) * 0.1f, 0, 232f / 255f); // 파랑 값 미세하게 감소
        // _sp.color = c;

        if (transform.localScale.x < 0f)
        {
            Destroy(gameObject);
        }
    }
}