using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceDefender.Core;
namespace SpaceDefender.Enemy
{
    public class EnemyMover : MonoBehaviour
    {

        private WaveConfig _waveConfig;
        private List<Transform> _wayPoint;
        private int _wavePointIndex = 0;


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
                transform.position = Vector2.MoveTowards(transform.position, targetPos, MoveThisFrame);

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
    }
}



