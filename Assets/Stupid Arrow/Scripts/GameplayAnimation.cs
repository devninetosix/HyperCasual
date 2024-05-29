using UnityEngine;

public class GameplayAnimation : MonoBehaviour
{
    private float _speed;
    public PlayerLogic pl;
    public CircleCollider2D playersCollider;
    private GameplayAnimation _gameplayAnimation;

    private void Start()
    {
        _gameplayAnimation = GetComponent<GameplayAnimation>();
    }

    private void FixedUpdate()
    {
        _speed += 0.001f;
        transform.localScale = new Vector2(
            transform.localScale.x + (0.0005f + _speed),
            transform.localScale.y + (0.0005f + _speed)
        );

        if (!(transform.localScale.x >= 1))
        {
            return;
        }

        transform.localScale = new Vector2(1, 1);
        pl.enabled = true;
        playersCollider.enabled = true;
        _gameplayAnimation.enabled = false;
    }
}