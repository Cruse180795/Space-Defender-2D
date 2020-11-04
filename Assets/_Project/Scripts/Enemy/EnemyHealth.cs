using SpaceDefender.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDefender.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private int _enemyHealth;
        [SerializeField] private int _pointsPerDeath = 10;


        private PlayerHealth _playerHealth;
        private PlayerScore _playerScore;

        private void Start()
        {
            _playerHealth = FindObjectOfType<PlayerHealth>();
            _playerScore = FindObjectOfType<PlayerScore>();

            if (_playerHealth == null)
            {
                Debug.LogError("The PlayerHealth Is NULL");
            }

            if(_playerScore == null)
            {
                Debug.LogError("The PlayerScore Is NULL");
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if(_playerHealth != null)
                {
                    _playerHealth.PlayerDamage();
                    Destroy(this.gameObject);
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
                if (_playerScore != null)
                {
                    _playerScore.AddToScore(_pointsPerDeath);
                }

                Destroy(this.gameObject);
            }
        }
    }
}


