using SpaceDefender.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SpaceDefender.Manager
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Slider _healthBarSliderUI;
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
            PlayerScore.score += UpdateScoreText;
            _gameOverPanel.SetActive(false);


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

        private void UpdateScoreText(int score)
        {
            _scoreText.text = " " + score.ToString();
        }

        private void ShowGameOverPanel()
        {
            if(_gameOverPanel != null)
            {
                _gameOverPanel.SetActive(true);
            }
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


