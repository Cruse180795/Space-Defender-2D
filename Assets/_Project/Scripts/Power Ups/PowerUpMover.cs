using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDefender.PowerUps
{
    public class PowerUpMover : MonoBehaviour
    {
        [SerializeField] private float _powerUpMoveSpeed = 5f;


        private void Update()
        {
            Movement();
        }

        private void Movement()
        {
            transform.Translate(Vector3.down * _powerUpMoveSpeed * Time.deltaTime);

            if(transform.position.y < -6f)
            {
                Destroy(this.gameObject);
            }
        }
    }
}

