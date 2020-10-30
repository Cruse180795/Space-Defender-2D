using SpaceDefender.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDefender.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int _enemyHealth;


        private PlayerHealth _playerHealth;


        private void Start()
        {
            _playerHealth = FindObjectOfType<PlayerHealth>();

            if (_playerHealth == null)
            {
                Debug.LogError("The PlayerHealth Is NULL");
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if(_playerHealth != null)
                {
                    _playerHealth.PlayerDamage();
                    HandleEnemyDeath();
                }
            }

            if(collision.CompareTag("Player Projectile"))
            {
                Destroy(collision.gameObject);
                HandleEnemyDeath();
            }
        }


        private void HandleEnemyDeath()
        {
            _enemyHealth--;

            if(_enemyHealth <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}


