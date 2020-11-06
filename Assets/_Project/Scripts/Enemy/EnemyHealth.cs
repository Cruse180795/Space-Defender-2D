using SpaceDefender.Player;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceDefender.Enemy
{
    public class EnemyHealth : MonoBehaviour
    {
        [Header("Enemy Health Config")]
        [SerializeField] private int _enemyHealth;
        [SerializeField] private int _pointsPerDeath = 10;
        [SerializeField] private Slider _enemyHealthSlider;


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

            _enemyHealthSlider.maxValue = _enemyHealth;
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

            _enemyHealthSlider.value = _enemyHealth;

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


