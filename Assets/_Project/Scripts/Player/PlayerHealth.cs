using SpaceDefender.PowerUps;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace SpaceDefender.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [Header("Player Health Config")]
        [SerializeField] private int _playerHealth = 3;


        [SerializeField]private int _shieldHP = 3;
        private PowerUpBehaviour _behaviour;


        public delegate void Health(int health);
        public static event Health health;

        public delegate void PlayerDeath();
        public static event PlayerDeath playerDeath;


        private Animator _animator;
        private CircleCollider2D _circleCollider2D;
        PlayerMover _playerMover;


        private void Start()
        {
            _behaviour = GetComponent<PowerUpBehaviour>();
            _animator = GetComponent<Animator>();
            _circleCollider2D = GetComponent<CircleCollider2D>();
            _playerMover = GetComponent<PlayerMover>();

            if (_behaviour == null)
            {
                Debug.LogError("The PowerUpBehaviour Script Is NULL");
            }

            if(_animator == null)
            {
                Debug.LogError("The Anmimator Is NULL");
            }

            if(_circleCollider2D == null)
            {
                Debug.LogError("The CircleCollider Is NULL");
            }

            if(_playerMover == null)
            {
                Debug.LogError("The PlayerMover Is NULL");
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Enemy Projectile"))
            {
                Destroy(collision.transform.parent.gameObject);

                if (_behaviour.IsShieldBoostActive == true)
                {
                    PlayerShield();
                    return;
                }

                _playerHealth--;

                if (health != null)
                {
                    health(_playerHealth);
                }

                HandlePlayerDeath();
                
            }
        }

        public void PlayerDamage()
        {

            if(_behaviour.IsShieldBoostActive == true)
            {
                PlayerShield();
                return;
            }

            _playerHealth--;


            if(health != null)
            {
                health(_playerHealth);
            }

            HandlePlayerDeath();
        }

        private void PlayerShield()
        {
            _shieldHP--;

            if (_shieldHP == 0)
            {
                _behaviour.IsShieldBoostActive = false;
                _behaviour.HidePlayerShieldUi();
            }

            return;
        }

        private void HandlePlayerDeath()
        {
            if (_playerHealth <= 0)
            {
                if(playerDeath != null)
                {
                    playerDeath();
                }
                _animator.SetTrigger("IsDead");
                _circleCollider2D.enabled = false;
                _playerMover.Speed = 0;
                Destroy(this.gameObject, 2f);
            }
        }
    }
}

