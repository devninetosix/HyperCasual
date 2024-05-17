using System;
using Assets.PixelFantasy.Common.Scripts;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.CharacterScripts;
using UnityEngine;

namespace Assets.PixelFantasy.PixelHeroes.Common.Scripts.ExampleScripts
{
    [RequireComponent(typeof(Character))]
    public class CharacterAnimation : MonoBehaviour
    {
        private Character _character;
        
        public void Start()
        {
            _character = GetComponent<Character>();
            Idle();
        }

        public void Idle()
        {
            SetState(CharacterState.Idle);
        }

        public void Ready()
        {
            if (GetState() == CharacterState.Run)
            {
                EffectManager.Instance.CreateSpriteEffect(_character, "Brake");
            }

            SetState(CharacterState.Ready);
        }

        public void Run()
        {
            if (GetState() != CharacterState.Run)
            {
                EffectManager.Instance.CreateSpriteEffect(_character, "Run");
            }

            SetState(CharacterState.Run);
        }

        public void Jump()
        {
            SetState(CharacterState.Jump);
            EffectManager.Instance.CreateSpriteEffect(_character, "Jump");
        }

        public void Fall()
        {
            SetState(CharacterState.Fall);
        }

        public void Land(CharacterState state = CharacterState.Land)
        {
            SetState(state);
            EffectManager.Instance.CreateSpriteEffect(_character, "Fall");
        }

        public void Block()
        {
            SetState(CharacterState.Block);
        }

        public void Climb()
        {
            SetState(CharacterState.Climb);
        }

        public void Die()
        {
            SetState(CharacterState.Die);
        }

        public void Roll()
        {
            _character.Animator.SetTrigger("Roll");
            EffectManager.Instance.CreateSpriteEffect(_character, "Dash");
        }

        public void Slash()
        {
            _character.Animator.SetTrigger("Slash");
        }

        public void Jab()
        {
            _character.Animator.SetTrigger("Jab");
        }

        public void Push()
        {
            _character.Animator.SetTrigger("Push");
        }

        public void Shot()
        {
            _character.Animator.SetTrigger("Shot");
        }

        public void Hit()
        {
            _character.Animator.SetTrigger("Hit");
        }

        public void Crawl()
        {
            SetState(CharacterState.Crawl);
        }

        public void Crouch()
        {
            SetState(CharacterState.Crouch);
        }

        public void SetState(CharacterState state)
        {
            foreach (var variable in new[] { "Idle", "Ready", "Walk", "Run", "Crouch", "Crawl", "Jump", "Fall", "Land", "Block", "Climb", "Die" })
            {
                _character.Animator.SetBool(variable, false);
            }

            switch (state)
            {
                case CharacterState.Idle: _character.Animator.SetBool("Idle", true); break;
                case CharacterState.Ready: _character.Animator.SetBool("Ready", true); break;
                case CharacterState.Walk: _character.Animator.SetBool("Walk", true); break;
                case CharacterState.Run: _character.Animator.SetBool("Run", true); break;
                case CharacterState.Crouch: _character.Animator.SetBool("Crouch", true); break;
                case CharacterState.Crawl: _character.Animator.SetBool("Crawl", true); break;
                case CharacterState.Jump: _character.Animator.SetBool("Jump", true); break;
                case CharacterState.Fall: _character.Animator.SetBool("Fall", true); break;
                case CharacterState.Land: _character.Animator.SetBool("Land", true); break;
                case CharacterState.Block: _character.Animator.SetBool("Block", true); break;
                case CharacterState.Climb: _character.Animator.SetBool("Climb", true); break;
                case CharacterState.Die: _character.Animator.SetBool("Die", true); break;
                default: throw new NotSupportedException(state.ToString());
            }

            //Debug.Log("SetState: " + state);
        }

        public CharacterState GetState()
        {
            if (_character.Animator.GetBool("Idle")) return CharacterState.Idle;
            if (_character.Animator.GetBool("Ready")) return CharacterState.Ready;
            if (_character.Animator.GetBool("Walk")) return CharacterState.Walk;
            if (_character.Animator.GetBool("Run")) return CharacterState.Run;
            if (_character.Animator.GetBool("Crawl")) return CharacterState.Crawl;
            if (_character.Animator.GetBool("Crouch")) return CharacterState.Crouch;
            if (_character.Animator.GetBool("Jump")) return CharacterState.Jump;
            if (_character.Animator.GetBool("Fall")) return CharacterState.Fall;
            if (_character.Animator.GetBool("Land")) return CharacterState.Land;
            if (_character.Animator.GetBool("Block")) return CharacterState.Block;
            if (_character.Animator.GetBool("Climb")) return CharacterState.Climb;
            if (_character.Animator.GetBool("Die")) return CharacterState.Die;

            return CharacterState.Ready;
        }

        public void SetBool(string paramName)
        {
            _character.Animator.SetBool(paramName, true);
        }

        public void UnsetBool(string paramName)
        {
            _character.Animator.SetBool(paramName, false);
        }
    }
}