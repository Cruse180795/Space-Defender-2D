using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceDefender.Player;

namespace SpaceDefender.PowerUps
{
    public class PowerUpMover : MonoBehaviour
    {
        [SerializeField] private float _powerUpMoveSpeed = 5f;


        private PowerUpType _powerUpType;
        private SetPlayerPostition _playerPos;

        private void Start()
        {
            _powerUpType = GetComponent<PowerUpType>();
            _playerPos = FindObjectOfType<SetPlayerPostition>();

            if(_powerUpType == null)
            {
                Debug.LogError("The PowerUpType Is NULL");
            }

            if(_playerPos == null)
            {
                Debug.LogError("The PlayerPos Is NULL");
            }
        }

        private void Update()
        {
            Movement();
        }

        private void Movement()
        {
            if(_powerUpType.CanCollect == false)
            {
                transform.Translate(Vector3.down * _powerUpMoveSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, _playerPos.transform.position, 5f * Time.deltaTime);
            }

            

            if(transform.position.y < -6f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}

