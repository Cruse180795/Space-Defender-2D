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

        private WaitForSeconds _tripleShotTimer;
        private WaitForSeconds _speedBoostTimer;

        private UIManager _uiManager;

        private void Start()
        {
            _uiManager = FindObjectOfType<UIManager>();

            if(_uiManager == null)
            {
                Debug.LogError("The UIManager Is NULL");
            }

            _currentAmmoCount = _maxPlayerAmmo;

            _tripleShotTimer = new WaitForSeconds(_tripleShotCoolDownTimer);
            _speedBoostTimer = new WaitForSeconds(_speedBoostCoolDownTimer);
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

        
    }

}

