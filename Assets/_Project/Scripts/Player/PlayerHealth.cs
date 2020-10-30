using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDefender.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [Header("Player Health Config")]
        [SerializeField] private int _playerHealth = 3;



        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("Enemy Projectile"))
            {
                Destroy(collision.transform.parent.gameObject);
                _playerHealth--;
                HandlePlayerDeath();
                
            }
        }

        public void PlayerDamage()
        {
            _playerHealth--;
            HandlePlayerDeath();
        }

        private void HandlePlayerDeath()
        {
            if (_playerHealth <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}

