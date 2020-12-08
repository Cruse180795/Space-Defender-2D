using SpaceDefender.Player;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;
using SpaceDefender.Core;
using SpaceDefender.Manager;

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

        [Header("Enemy Shield Config")]
        [SerializeField] private Slider _enemyShieldSlider;
        [SerializeField] private int _shieldHP;
        [SerializeField] private GameObject _enemyShieldUI;
        [SerializeField] private bool _canUseShield;

        private PlayerHealth _playerHealth;
        private PlayerScore _playerScore;


        private EnemyShooting _enemyShooting;

        private WaveConfig _waveConfig;
        private UIManager _uiManager;

        private Animator _animator;
        private void Start()
        {
            _playerHealth = FindObjectOfType<PlayerHealth>();
            _playerScore = FindObjectOfType<PlayerScore>();
            _enemyShooting = GetComponent<EnemyShooting>();
            _uiManager = FindObjectOfType<UIManager>();
            _waveConfig = FindObjectOfType<WaveConfig>();
            if(_uiManager == null)
            {
                Debug.LogError("The UIManager Is NULL");
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

            if(_canUseShield == true)
            {
                _enemyShieldSlider.maxValue = _shieldHP;
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

                if (_canUseShield == true)
                {
                    EnemyShield();
                    
                    
                }
                else if (_canUseShield == false)
                {
                    HandleEnemyDeath();
                }
                
            }

            if(collision.CompareTag("Shock Wave Projectile"))
            {
                Destroy(collision.gameObject);
                _enemyHealth--;
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

        private void EnemyShield()
        {
            _shieldHP--;
            _enemyShieldSlider.value = _shieldHP;

            if (_shieldHP == 0)
            {
                Destroy(_enemyShieldSlider.gameObject);
                _enemyShieldUI.SetActive(false);
                _canUseShield = false;

            }
        }
    }
}


