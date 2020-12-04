using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceDefender.Core;
namespace SpaceDefender.Enemy
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private bool _canAvoidPlayer = false;
        [SerializeField] private float _detectionRange = 3f;
        [SerializeField] private float _dodgeSpeedMultiplier = 1.5f;

        private WaveConfig _waveConfig;
        private List<Transform> _wayPoint;
        private int _wavePointIndex = 0;

        private bool _avoidPlayer = false;

        private void Start()
        {
            _wayPoint = _waveConfig.GetWayPoints();
            transform.position = _wayPoint[_wavePointIndex].transform.position;
        }


        private void Update()
        {
            EnemyMovement();
        }

        public void SetWaveConfig(WaveConfig waveConfig)
        {
            this._waveConfig = waveConfig;
        }

        private void EnemyMovement()
        {
            if(_wavePointIndex <= _wayPoint.Count - 1)
            {
                var targetPos = _wayPoint[_wavePointIndex].transform.position;
                var MoveThisFrame = _waveConfig.GetMoveSpeed() * Time.deltaTime;
                var dodgeProjectile = _waveConfig.GetMoveSpeed() * _dodgeSpeedMultiplier * Time.deltaTime;

                if(_avoidPlayer == true)
                {
                    transform.position = Vector2.MoveTowards(transform.position, targetPos, dodgeProjectile);
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, targetPos, MoveThisFrame);
                }
                

                if(transform.position == targetPos)
                {
                    _wavePointIndex++;
                }
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        private void FixedUpdate()
        {
            if(_canAvoidPlayer == true)
            {
                DetectPlayerProjectile();
            }
            
        }

        private void DetectPlayerProjectile()
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, _detectionRange);

            if(collider.CompareTag("Player Projectile") || collider.CompareTag("Shock Wave Projectile"))
            {
                _avoidPlayer = true;
            }
        }
    }
}



