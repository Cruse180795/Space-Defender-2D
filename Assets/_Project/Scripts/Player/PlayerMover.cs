using SpaceDefender.PowerUps;
using SpaceDefender.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDefender.Player
{
    public class PlayerMover : MonoBehaviour
    {
        [Header("Player Movement Config")]
        [SerializeField] private float _playerMoveSpeed = 10f;
        [SerializeField] private float _playerBoostSpeed = 15f;
        [SerializeField] private float _topBound;
        [SerializeField] private float _bottomBound;
        [SerializeField] private float _rightBound;
        [SerializeField] private float _leftBound;
        [SerializeField] private GameObject _playerThrusterUI;

        [Header("Player Thruster Seetings")]
        [SerializeField] private float _maxFuel = 100f;
        [SerializeField] private float _reduceRate = 20f;
        [SerializeField] private float _regenRate = 40f;
        [SerializeField] private float _regenCoolDown = 3f;

        public float Speed
        {
            get
            {
                return _playerMoveSpeed;
            }

            set
            {
                _playerMoveSpeed = value;
            }
        }


        private float _currentSpeed;
        private bool _canUseFuel = true;
        private float _currentFuelAmount;
        private float _fuelCoolDownTimer;

        private PowerUpBehaviour _behaviour;
        private UIManager _uiManager;

        private void Start()
        {
            _behaviour = GetComponent<PowerUpBehaviour>();
            _uiManager = FindObjectOfType<UIManager>();

            if(_behaviour == null)
            {
                Debug.LogError("The PowerUpBehaviour Script Is Null");
            }

            if(_uiManager == null)
            {
                Debug.LogError("The UIManager Is NULL");
            }

            _currentSpeed = _playerMoveSpeed;
            _playerThrusterUI.SetActive(false);
            _currentFuelAmount = _maxFuel;
            _fuelCoolDownTimer = _regenCoolDown;
            

        }

        private void Update()
        {
            Movement();
            MovementBounds();
            BoostedMovement();

            _uiManager.UpdateFuelSlider(_currentFuelAmount);
        }


        private void Movement()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 moveDirection = new Vector3(horizontalInput, verticalInput);

            transform.Translate(moveDirection * _currentSpeed * Time.deltaTime);

        }

        private void MovementBounds()
        {
            transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, _bottomBound, _topBound), 0f);

            if(transform.position.x > _rightBound)
            {
                transform.position = new Vector3(_leftBound, transform.position.y, 0f);
            }
            else if (transform.position.x < _leftBound)
            {
                transform.position = new Vector3(_rightBound, transform.position.y, 0f);
            }
        }

        private void BoostedMovement()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                UseFuel();
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                RefillFuel();
            }

            RegenCoolDown();
        }

        private void UseFuel()
        {
            _currentSpeed = _playerBoostSpeed;
            _playerThrusterUI.SetActive(true);
            _canUseFuel = true;
            _currentFuelAmount -= _reduceRate * Time.deltaTime;
            _regenCoolDown = _fuelCoolDownTimer;
        }

        private void RefillFuel()
        {
            _currentSpeed = _playerMoveSpeed;
            _playerThrusterUI.SetActive(false);
            _canUseFuel = false;
        }

        private void RegenCoolDown()
        {
            _regenCoolDown -= Time.deltaTime;
            
            if(_regenCoolDown <= 0 && _canUseFuel == false)
            {
                _regenCoolDown = 0f;
                RegenFuel();
            }

        }

        private void RegenFuel()
        {
            if(_currentFuelAmount < _maxFuel)
            {
                _currentFuelAmount += _regenRate * Time.deltaTime;
            }
        }
    }

}


