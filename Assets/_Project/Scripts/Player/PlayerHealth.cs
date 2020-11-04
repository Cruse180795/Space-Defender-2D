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


        private void Start()
        {
            _behaviour = GetComponent<PowerUpBehaviour>();

            if (_behaviour == null)
            {
                Debug.LogError("The PowerUpBehaviour Script Is NULL");
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

                Destroy(this.gameObject);
            }
        }
    }
}

