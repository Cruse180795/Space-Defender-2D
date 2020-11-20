using SpaceDefender.PowerUps;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace SpaceDefender.Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [Header("Player Shooting Config")]
        [SerializeField] private GameObject _playerProjectilePrefab;
        [SerializeField] private GameObject _tripleShotPrefab;
        [SerializeField] private float _fireRate = 0.25f;
        [SerializeField] private int _ammoCount = 15;

        private float _nextFire = -1f;
        private bool _canShoot = true;

        public delegate void AmmoCount(int ammoCounty);
        public static event AmmoCount ammoCount;


        private PowerUpBehaviour _behaviour;

        private void Start()
        {
            _behaviour = GetComponent<PowerUpBehaviour>();

            if(_behaviour == null)
            {
                Debug.LogError("The PowerUpBehaviour Script Is NULL");
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && Time.time > _nextFire && _canShoot == true)
            {
                FireProjectile();
            }

            
        }

        private void FireProjectile()
        {
            _nextFire = Time.time + _fireRate;

            if(_behaviour.IsTripleShotActive == true)
            {
                Instantiate(_tripleShotPrefab, transform.position, quaternion.identity);
            }
            else
            {
                Vector3 offset = new Vector3(0f, 0.8f, 0f);
                Instantiate(_playerProjectilePrefab, transform.position + offset, Quaternion.identity);
            }

            _ammoCount--;

            if(ammoCount != null)
            {
                ammoCount(_ammoCount);
            }


            if(_ammoCount <= 0)
            {
                _canShoot = false;
            }
        }
    }
}


