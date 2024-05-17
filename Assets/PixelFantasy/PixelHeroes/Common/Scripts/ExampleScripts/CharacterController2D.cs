using Assets.PixelFantasy.PixelHeroes.Common.Scripts.CharacterScripts;
using System.Linq;
using UnityEngine;

namespace Assets.PixelFantasy.PixelHeroes.Common.Scripts.ExampleScripts
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CharacterAnimation))]
    public class CharacterController2D : MonoBehaviour
    {
        public Vector2 Input;
        public bool IsGrounded;

        public float Acceleration;
        public float MaxSpeed;
        public float JumpForce;
        public float Gravity;

        private Collider2D _collider;
        private Rigidbody2D _rigidbody;
        private CharacterAnimation _animation;
        
        private bool _jump;
        private bool _crouch;

        public void Start()
        {
            _collider = GetComponent<Collider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _animation = GetComponent<CharacterAnimation>();
        }

        public void FixedUpdate()
        {
            var state = _animation.GetState();

            if (state == CharacterState.Die || state == CharacterState.Block || state == CharacterState.Climb) return;
            
            var velocity = _rigidbody.velocity;

            if (Input.x == 0)
            {
                if (IsGrounded)
                {
                    velocity.x = Mathf.MoveTowards(velocity.x, 0, Acceleration * 3 * Time.fixedDeltaTime);
                }
            }
            else
            {
                var maxSpeed = MaxSpeed;
                var acceleration = Acceleration;

                if (_jump)
                {
                    acceleration /= 2;
                }
                else if (_crouch)
                {
                    acceleration /= 2;
                    maxSpeed /= 4;
                }

                velocity.x = Mathf.MoveTowards(velocity.x, Input.x * maxSpeed, acceleration * Time.fixedDeltaTime);
                Turn(velocity.x);
            }
            
            if (IsGrounded)
            {
                _crouch = Input.y < 0;

                if (!_jump)
                {
                    if (Input.x == 0)
                    {
                        if (_crouch)
                        {
                            _animation.Crouch();
                        }
                        else
                        {
                            _animation.Ready();
                        }
                    }
                    else
                    {
                        if (_crouch)
                        {
                            _animation.Crawl();
                        }
                        else
                        {
                            _animation.Run();
                        }
                    }
                }

                if (Input.y > 0 && !_jump)
                {
                    _jump = true;
                    _rigidbody.AddForce(Vector2.up * JumpForce);
                    _animation.Jump();
                }
            }
            else
            {
                velocity.y -= Gravity * Time.fixedDeltaTime;

                if (velocity.y < 0)
                {
                    _jump = true;
                    _animation.Fall();
                }
            }

            _rigidbody.velocity = velocity;
        }

        private void Turn(float direction)
        {
            var scale = transform.localScale;

            scale.x = Mathf.Sign(direction) * Mathf.Abs(scale.x);

            transform.localScale = scale;
        }

        private Collider2D _ground;

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.contacts.All(i => i.point.y <= _collider.bounds.min.y))
            {
                IsGrounded = true;
                _ground = collision.collider;

                if (_jump)
                {
                    _jump = false;
                    _animation.Land(Input.y < 0 ? CharacterState.Crouch : CharacterState.Land);
                }
            }
        }

        public void OnCollisionExit2D(Collision2D collision)
        {
            if (IsGrounded && collision.collider == _ground)
            {
                IsGrounded = false;
            }
        }
    }
}