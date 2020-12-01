using SpaceDefender.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using SpaceDefender.Core;
using SpaceDefender.Enemy;

namespace SpaceDefender.Manager
{
    public class SpawnManager : MonoBehaviour
    {
        [Header("Enemy Spawn Config")]
        [SerializeField] private List<WaveConfig> _waveConfig;
        [SerializeField] private GameObject _enemyPrefabContainer;

        [Header("Power Up Spawn Config")]
        [SerializeField] private GameObject[] _powerUps;
        [SerializeField] private float _timeBetweenPowerUps = 7.5f;
        [SerializeField] private float _PowerUpSpawnWaitTimer = 5f;

        private int _startingWave = 0;

        private WaitForSeconds _timeBetweenPowerUpSpawns;
        private WaitForSeconds _powerUpDealyTimer;

        private bool _stopSpawning = false;

        private void Start()
        {

            _timeBetweenPowerUpSpawns = new WaitForSeconds(_timeBetweenPowerUps);
            _powerUpDealyTimer = new WaitForSeconds(_PowerUpSpawnWaitTimer);

            PlayerHealth.playerDeath += StopSpawning;

        }

        public void StartSpawning()
        {
            StartCoroutine(PowerUpSpawner());
            StartCoroutine(SpawnWaves());
        }

        private IEnumerator SpawnWaves()
        {
            while (_stopSpawning == false)
            {
                for (int waveIndex = 0; waveIndex < _waveConfig.Count; waveIndex++)
                {
                    var currentWave = _waveConfig[waveIndex];
                    yield return new WaitForSeconds(3f);
                    yield return StartCoroutine(SpawnEnemiesInWave(currentWave));
                }
            }
        }


        private IEnumerator SpawnEnemiesInWave(WaveConfig waveConfig)
        {
            for (int enemyCounter = 0; enemyCounter < waveConfig.GetEnemiesPerWave(); enemyCounter++)
            {
                var newEnemy = Instantiate(waveConfig.GetEnemy(), waveConfig.GetWayPoints()[0].transform.position, quaternion.identity);
                newEnemy.GetComponent<EnemyMover>().SetWaveConfig(waveConfig);
                newEnemy.transform.parent = _enemyPrefabContainer.transform;
                yield return new WaitForSeconds(waveConfig._GetTimeBetweenSpawns());
            }
        }

        private IEnumerator PowerUpSpawner()
        {
            yield return _powerUpDealyTimer;

            while(_stopSpawning == false)
            {
                int randomPowerUpIndex = UnityEngine.Random.Range(0, _powerUps.Length);
                Instantiate(_powerUps[randomPowerUpIndex], SetRandomSpawnPosition(), quaternion.identity);
                yield return _timeBetweenPowerUpSpawns;
            }
        }

        private Vector3 SetRandomSpawnPosition()
        {
            Vector3 randomSpawnPos = new Vector3(UnityEngine.Random.Range(-7f, 7f), 6f, 0f);
            return randomSpawnPos;
        }

        private void StopSpawning()
        {
            _stopSpawning = true;
        }
    }
}



