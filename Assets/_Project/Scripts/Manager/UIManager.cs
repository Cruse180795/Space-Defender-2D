using SpaceDefender.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SpaceDefender.Enemy;

namespace SpaceDefender.Manager
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Slider _healthBarSliderUI;
        [SerializeField] private Slider _playerShieldSliderUI;
        [SerializeField] private Slider _playerFuelSliderUI;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private TextMeshProUGUI _countDownText;
        [SerializeField] private int _countDownTime;

        private SpawnManager _spawnManager;

        private void Start()
        {
            _spawnManager = FindObjectOfType<SpawnManager>();

            if(_spawnManager == null)
            {
                Debug.LogError("The SpawnManager Is NULL");
            }

            PlayerHealth.health += UpdateHealthBarSlider;
            PlayerHealth.health += SetHealthBarValue;
            PlayerHealth.playerDeath += ShowGameOverPanel;
            PlayerHealth.shieldHealth += SetShieldSlider;
            PlayerHealth.shieldHealth += UpdateShieldSlider;
            PlayerScore.score += UpdateScoreText;


            _gameOverPanel.SetActive(false);
            _countDownText.gameObject.SetActive(true);

            StartCoroutine(CountDownToStart());
        }


        private void UpdateHealthBarSlider(int healthValue)
        {
            _healthBarSliderUI.value -= healthValue;
        }

        private void SetHealthBarValue(int healthValue)
        {
            _healthBarSliderUI.value = healthValue;
        }


        private void UpdateShieldSlider(int shieldHealth)
        {
            _playerShieldSliderUI.value = shieldHealth;
        }

        private void SetShieldSlider(int shieldHealth)
        {
            _playerShieldSliderUI.value = shieldHealth;
        }


        public void UpdateFuelSlider(float fuelAmount)
        {
            _playerFuelSliderUI.value = fuelAmount;
        }

        private void UpdateScoreText(int score)
        {
            _scoreText.text = "Score : " + score.ToString();
        }

        private void ShowGameOverPanel()
        {
            if(_gameOverPanel != null)
            {
                StartCoroutine(ShowGameOver());
            }
        }


        private IEnumerator ShowGameOver()
        {
            yield return new WaitForSeconds(2f);

            _gameOverPanel.SetActive(true);
        }

        private IEnumerator CountDownToStart()
        {
            while (_countDownTime > 0)
            {
                _countDownText.text = _countDownTime.ToString();

                yield return new WaitForSeconds(1f);

                _countDownTime--;
            }

            _countDownText.text = "START!";
            _spawnManager.StartSpawning();

            yield return new WaitForSeconds(1f);

            _countDownText.gameObject.SetActive(false);
        }
    }
}


