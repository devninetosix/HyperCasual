using UnityEngine;

public class PlayerDestroy : MonoBehaviour
{
    private float _timer;
    private float _scale = 1;

    private void Update()
    {
        _timer += Time.deltaTime;
        
        if (_timer >= 0.01f)
        {
            _timer = 0;
            _scale -= 0.05f;
            transform.localScale = new Vector2(_scale, _scale);
            if (_scale <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}