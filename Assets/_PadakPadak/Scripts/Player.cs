using UnityEngine;


namespace PadakPadak
{
    public class Player : MonoBehaviour
    {
        public Sprite[] sprites;
        public float strength = 5f;
        public float gravity = -9.81f;
        public float tilt = 5f;

        private SpriteRenderer _spriteRenderer;
        private Vector3 _direction;
        private int _spriteIndex;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
        }

        private void OnEnable()
        {
            Vector3 position = transform.position;
            position.y = 0f;
            transform.position = position;
            _direction = Vector3.zero;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                _direction = Vector3.up * strength;
            }

            // Apply gravity and update the position
            _direction.y += gravity * Time.deltaTime;
            transform.position += _direction * Time.deltaTime;

            // Tilt the bird based on the direction
            Vector3 rotation = transform.eulerAngles;
            rotation.z = _direction.y * tilt;
            transform.eulerAngles = rotation;
        }

        private void AnimateSprite()
        {
            _spriteIndex++;

            if (_spriteIndex >= sprites.Length)
            {
                _spriteIndex = 0;
            }

            if (_spriteIndex < sprites.Length && _spriteIndex >= 0)
            {
                _spriteRenderer.sprite = sprites[_spriteIndex];
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag($"Obstacle"))
            {
                GameManager.Instance.GameOver();
            }
            else if (other.gameObject.CompareTag($"Scoring"))
            {
                GameManager.Instance.IncreaseScore();
            }
        }
    }
}