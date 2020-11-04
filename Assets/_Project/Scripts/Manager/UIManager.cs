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

        private void Start()
        {
            PlayerHealth.health += UpdateHealthBarSlider;
            PlayerHealth.health += SetHealthBarValue;
            PlayerHealth.playerDeath += ShowGameOverPanel;
            PlayerScore.score += UpdateScoreText;
            _gameOverPanel.SetActive(false);
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
    }
}


