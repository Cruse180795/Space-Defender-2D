using SpaceDefender.PowerUps;
using SpaceDefender.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml.Schema;

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

        public delegate void ShieldHealth(int shieldHealth);
        public static event ShieldHealth shieldHealth;

        private AudioSource _audioSource;
        private Animator _animator;
        private CircleCollider2D _circleCollider2D;

        private PlayerMover _playerMover;
        private CameraShake _cameraShake;


        private void Start()
        {
            _behaviour = GetComponent<PowerUpBehaviour>();
            _animator = GetComponent<Animator>();
            _circleCollider2D = GetComponent<CircleCollider2D>();
            _playerMover = GetComponent<PlayerMover>();
            _audioSource = GetComponent<AudioSource>();
            _cameraShake = FindObjectOfType<CameraShake>();

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

            if (_audioSource == null)
            {
                Debug.LogError("The AudioSource Is NULL");
            }

            if(_cameraShake == null)
            {
                Debug.LogError("The CameraShake Is NULL");
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
                _cameraShake.TriggerCameraShake();

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
            _cameraShake.TriggerCameraShake();

            if (health != null)
            {
                
                health(_playerHealth);
            }

            HandlePlayerDeath();
        }

        private void PlayerShield()
        {
            _shieldHP--;

            if(shieldHealth != null)
            {
                shieldHealth(_shieldHP);
            }

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
                _audioSource.Play();
                Destroy(this.gameObject, 2f);
            }
        }
    }
}

