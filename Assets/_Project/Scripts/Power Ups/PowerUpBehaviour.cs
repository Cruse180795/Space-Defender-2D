using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

namespace SpaceDefender.PowerUps
{
    public class PowerUpBehaviour : MonoBehaviour
    {
        [Header("Power Up CoolDowns")]
        [SerializeField] private float _tripleShotCoolDownTimer = 5f;
        [SerializeField] private float _speedBoostCoolDownTimer = 7.5f;

        [Header("Power Up UI")]
        [SerializeField] private GameObject _playerShieldUI;
        [SerializeField] private GameObject _playerThrusterUI;

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

        public bool IsSpeedBoostActive
        {
            get
            {
                return _isSpeedBoostActive;
            }

            set
            {
                _isSpeedBoostActive = value;
            }
        }
        private bool _isSpeedBoostActive = false;

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



        private void Start()
        {
            _tripleShotTimer = new WaitForSeconds(_tripleShotCoolDownTimer);
            _speedBoostTimer = new WaitForSeconds(_speedBoostCoolDownTimer);
            _playerShieldUI.SetActive(false);
            _playerThrusterUI.SetActive(false);
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

        public void UseSpeedBoost()
        {
            _isSpeedBoostActive = true;
            _playerThrusterUI.SetActive(true);
            StartCoroutine(SpeedBoostCoolDown());
        }


        private IEnumerator SpeedBoostCoolDown()
        {
            yield return _speedBoostTimer;
            _playerThrusterUI.SetActive(false);
            _isSpeedBoostActive = false;
        }

        public void UseShieldBoost()
        {
            _isShieldBoostActive = true;
            _playerShieldUI.SetActive(true);
        }

        public void HidePlayerShieldUi()
        {
            _playerShieldUI.SetActive(false);
        }
    }

}

