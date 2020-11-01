using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDefender.PowerUps
{
    public class PowerUpBehaviour : MonoBehaviour
    {
        [Header("Power Up CoolDowns")]
        [SerializeField] private float _tripleShotCoolDownTimer = 5f;


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

        private WaitForSeconds _tripleShotTimer;


        private void Start()
        {
            _tripleShotTimer = new WaitForSeconds(_tripleShotCoolDownTimer);
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

    }

}

