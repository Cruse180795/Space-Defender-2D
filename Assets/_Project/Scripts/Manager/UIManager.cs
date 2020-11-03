using SpaceDefender.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceDefender.Manager
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Slider _healthBarSliderUI;


        private void Start()
        {
            PlayerHealth.health += UpdateHealthBarSlider;
            PlayerHealth.health += SetHealthBarValue;
        }


        private void UpdateHealthBarSlider(int healthValue)
        {
            _healthBarSliderUI.value -= healthValue;
        }

        private void SetHealthBarValue(int healthValue)
        {
            _healthBarSliderUI.value = healthValue;
        }
    }
}


