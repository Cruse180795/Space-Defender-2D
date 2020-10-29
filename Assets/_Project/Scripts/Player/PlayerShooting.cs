using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDefender.Player
{
    public class PlayerShooting : MonoBehaviour
    {
        [Header("Player Shooting Config")]
        [SerializeField] private GameObject _playerProjectilePrefab;
        [SerializeField] private float _fireRate = 0.25f;

        private float _nextFire = -1f;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && Time.time > _nextFire)
            {
                FireProjectile();
            }

            
        }

        private void FireProjectile()
        {
            _nextFire = Time.time + _fireRate;
            Vector3 offset = new Vector3(0f, 0.8f, 0f);

            Instantiate(_playerProjectilePrefab, transform.position + offset, Quaternion.identity);
        }
    }
}


