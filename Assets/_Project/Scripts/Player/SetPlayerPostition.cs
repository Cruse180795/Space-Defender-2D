using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDefender.Player
{
    public class SetPlayerPostition : MonoBehaviour
    {
        [SerializeField] private Vector3 _startingPosition;

        private void Start()
        {
            transform.position = _startingPosition;
        }
    }

}

