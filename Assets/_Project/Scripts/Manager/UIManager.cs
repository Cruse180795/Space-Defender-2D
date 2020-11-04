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

        private void Start()
        {
            PlayerHealth.health += UpdateHealthBarSlider;
            PlayerHealth.health += SetHealthBarValue;
            PlayerScore.score += UpdateScoreText;
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
    }
}


