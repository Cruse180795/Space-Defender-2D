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
        [SerializeField] private AudioClip _enemyDeathClip;
        [Range(0.1f, 1f)][SerializeField] private float _deathClipVolume = 0.75f;

        private PlayerHealth _playerHealth;
        private PlayerScore _playerScore;


        private EnemyMover _enemyMover;
        private EnemyShooting _enemyShooting;

        private Animator _animator;
        private void Start()
        {
            _playerHealth = FindObjectOfType<PlayerHealth>();
            _playerScore = FindObjectOfType<PlayerScore>();
            _enemyMover = GetComponent<EnemyMover>();
            _enemyShooting = GetComponent<EnemyShooting>();

            if(_enemyMover == null)
            {
                Debug.LogError("The Enemy Mover Is NULL");
            }

            if(_enemyShooting == null)
            {
                Debug.Log("The EnemyShooting Is NULL");
            }


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

            if(collision.CompareTag("Shock Wave Projectile"))
            {
                Destroy(collision.gameObject);
                _enemyShooting.enabled = false;
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
                AudioSource.PlayClipAtPoint(_enemyDeathClip, Camera.main.transform.position, _deathClipVolume);

                Destroy(this.gameObject);
                
            }
        }
    }
}


