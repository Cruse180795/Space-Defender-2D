using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDefender.Core
{
    [CreateAssetMenu(menuName = "Enemy Wave Config")]

    public class WaveConfig : ScriptableObject
    {
        [SerializeField] private GameObject _enemyPath;
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private float _enemyMoveSpeed;
        [SerializeField] private float _timeBetweenSpawns;
        [SerializeField] private int _enemiesPerWave;



        public List<Transform> GetWayPoints()
        {
            var waveWayPoints = new List<Transform>();

            foreach (Transform child in _enemyPath.transform)
            {
                waveWayPoints.Add(child);
            }

            return waveWayPoints;
        }

        public GameObject GetEnemy()
        {
            return _enemyPrefab;
        }

        public float GetMoveSpeed()
        {
            return _enemyMoveSpeed;
        }

        public float _GetTimeBetweenSpawns()
        {
            return _timeBetweenSpawns;
        }

        public int GetEnemiesPerWave()
        {
            return _enemiesPerWave;
        }
    }
}

