using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace SpaceDefender.Enemy
{
    public class EnemyShooting : MonoBehaviour
    {
        [SerializeField] private GameObject _enemyProjectilePrefab;
        [SerializeField] private float _fireRate = 0.5f;

        private float _nextFire = -1f;



        private void Update()
        {
            if(Time.time > _nextFire)
            {
                FireProjectile();
            }
        }


        private void FireProjectile()
        {
            _nextFire = Time.time + _fireRate;
            Instantiate(_enemyProjectilePrefab, transform.position, quaternion.identity);
        }
    }
}

