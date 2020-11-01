using SpaceDefender.PowerUps;
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

        private float _currentSpeed;

        private PowerUpBehaviour _behaviour;

        private void Start()
        {
            _behaviour = GetComponent<PowerUpBehaviour>();
            if(_behaviour == null)
            {
                Debug.LogError("The PowerUpBehaviour Script Is Null");
            }

            _currentSpeed = _playerMoveSpeed;
        }

        private void Update()
        {
            Movement();
            MovementBounds();
        }


        private void Movement()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            Vector3 moveDirection = new Vector3(horizontalInput, verticalInput);

            if(_behaviour.IsSpeedBoostActive == true)
            {
                _currentSpeed = _playerBoostSpeed;
            }
            else
            {
                _currentSpeed = _playerMoveSpeed;
            }

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
    }

}


