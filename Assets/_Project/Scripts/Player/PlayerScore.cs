using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceDefender.Player
{
    public class PlayerScore : MonoBehaviour
    {

        public delegate void Score(int scoreValue);
        public static event Score score;

        private int _currentScore = 0;


        public void AddToScore(int scoreValue)
        {
            _currentScore += scoreValue;

            if(score != null)
            {
                score(_currentScore);
            }
        }


    }
}


