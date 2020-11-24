using SpaceDefender.Player;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using SpaceDefender.Manager;

namespace SpaceDefender.PowerUps
{
    public class PowerUpBehaviour : MonoBehaviour
    {
        [Header("Power Up CoolDowns")]
        [SerializeField] private float _tripleShotCoolDownTimer = 5f;
        [SerializeField] private float _speedBoostCoolDownTimer = 7.5f;
        [SerializeField] private float _shockWaveCoolDownTimer = 5f;

        [Header("Power Up UI")]
        [SerializeField] private GameObject _playerShieldUI;
        [SerializeField] private GameObject _playerShieldSlider;

        [Header("Player Ammo Config")]
        [SerializeField] private int _maxPlayerAmmo = 15;


        public int GetAmmoCount 
        {
            get
            {
                return _currentAmmoCount;
            }

            set
            {
                _currentAmmoCount = value;
                _uiManager.UpdateAmmoDisplay(_currentAmmoCount);
            }
            
        }
        private int _currentAmmoCount;
        
        public bool IsTripleShotActive
        {
            get
            {
                return _isTripleShotActive;
            }

            set
            {
                _isTripleShotActive = value;
            }
        }
        private bool _isTripleShotActive = false;

        public bool IsShieldBoostActive
        {
            get
            {
                return _isShieldBoostActive;
            }

            set
            {
                _isShieldBoostActive = value;
            }
        }
        private bool _isShieldBoostActive = false;

        public bool IsShockWaveActive
        {
            get
            {
                return _isShockWaveActive;
            }

            set
            {
                _isShockWaveActive = value;
            }
        }
        private bool _isShockWaveActive = false;

        private WaitForSeconds _tripleShotTimer;
        private WaitForSeconds _speedBoostTimer;
        private WaitForSeconds _shockWaveTimer;

        private UIManager _uiManager;
        private PlayerHealth _playerHealth;


        private void Start()
        {
            _uiManager = FindObjectOfType<UIManager>();
            _playerHealth = GetComponent<PlayerHealth>();

            if(_uiManager == null)
            {
                Debug.LogError("The UIManager Is NULL");
            }

            if(_playerHealth == null)
            {
                Debug.LogError("The PlayerHealth Is NULL");
            }

            _currentAmmoCount = _maxPlayerAmmo;

            _tripleShotTimer = new WaitForSeconds(_tripleShotCoolDownTimer);
            _speedBoostTimer = new WaitForSeconds(_speedBoostCoolDownTimer);
            _shockWaveTimer = new WaitForSeconds(_shockWaveCoolDownTimer);
            _playerShieldUI.SetActive(false);
            _playerShieldSlider.SetActive(false);

        }

        public void UseTripleShot()
        {
            _isTripleShotActive = true;
            StartCoroutine(TripleShotCoolDown());
        }


        private IEnumerator TripleShotCoolDown()
        {
            yield return _tripleShotTimer;
            _isTripleShotActive = false;
        }


        public void UseShockWave()
        {
            _isShockWaveActive = true;
            StartCoroutine(ShockWaveCooldown());
        }

        private IEnumerator ShockWaveCooldown()
        {
            yield return _shockWaveTimer;
            _isShockWaveActive = false;
        }

        public void UseShieldBoost()
        {
            _isShieldBoostActive = true;
            _playerShieldUI.SetActive(true);
            _playerShieldSlider.SetActive(true);
        }

        public void HidePlayerShieldUi()
        {
            _playerShieldUI.SetActive(false);
            _playerShieldSlider.SetActive(false);
        }

        public void RefillAmmo()
        {
            _currentAmmoCount = _maxPlayerAmmo;
            _uiManager.UpdateAmmoDisplay(_currentAmmoCount);
        }

        public void RegainHealth()
        {
            if(_playerHealth.GetPlayerHealth < 3 && _playerHealth.GetIsDead == false)
            {
                _playerHealth.GetPlayerHealth++;
                _uiManager.UpdateHealthBarSlider(_playerHealth.GetPlayerHealth);
            }
        }

        
    }

}

