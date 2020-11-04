using SpaceDefender.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.WSA.Input;

namespace SpaceDefender.Manager
{
    public class GameManager : MonoBehaviour
    {

        private bool _isGameOver = false;


        private void Start()
        {
            PlayerHealth.playerDeath += GameOver;
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
            {
                SceneManager.LoadScene("Game Scene");
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }


        private void GameOver()
        {
            _isGameOver = true;
        }

    }
}


