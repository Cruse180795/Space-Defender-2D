using SpaceDefender.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace SpaceDefender.Manager
{
    public class SpawnManager : MonoBehaviour
    {
        [Header("Enemy Spawn Config")]
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private GameObject _enemyPrefabContainer;
        [SerializeField] private float _timeBetweenEnemies = 2f;
        [SerializeField] private float _enemySpawnWaitTimer = 3f;

        [Header("Power Up Spawn Config")]
        [SerializeField] private GameObject[] _powerUps;
        [SerializeField] private float _timeBetweenPowerUps = 7.5f;
        [SerializeField] private float _PowerUpSpawnWaitTimer = 5f;

        private WaitForSeconds _timeBetweenEnemySpawns;
        private WaitForSeconds _spawnDelayTimer;

        private WaitForSeconds _timeBetweenPowerUpSpawns;
        private WaitForSeconds _powerUpDealyTimer;

        private bool _stopSpawning = false;

        private void Start()
        {
            _timeBetweenEnemySpawns = new WaitForSeconds(_timeBetweenEnemies);
            _spawnDelayTimer = new WaitForSeconds(_enemySpawnWaitTimer);

            _timeBetweenPowerUpSpawns = new WaitForSeconds(_timeBetweenPowerUps);
            _powerUpDealyTimer = new WaitForSeconds(_PowerUpSpawnWaitTimer);

            PlayerHealth.playerDeath += StopSpawning;

            StartCoroutine(EnemySpawner());
            StartCoroutine(PowerUpSpawner());
        }


        private IEnumerator EnemySpawner()
        {
            yield return _spawnDelayTimer;

            while(_stopSpawning == false)
            {
                GameObject newEnemy = Instantiate(_enemyPrefab, SetRandomSpawnPosition(), quaternion.identity);
                newEnemy.transform.parent = _enemyPrefabContainer.transform;
                yield return _timeBetweenEnemySpawns;
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



