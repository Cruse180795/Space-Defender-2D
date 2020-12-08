using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpaceDefender.Player;

namespace SpaceDefender.Enemy
{
    public class AggressiveEnemyMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _moveTowardsPlayerSpeed = 5f;
        [SerializeField] private float _playerInRadius = 5f;
        

        private bool _canMoveTowards = false;

        private PlayerHealth _player;

        private void Start()
        {
            _player = FindObjectOfType<PlayerHealth>();
        }

        private void Update()
        {
            Movement();
        }

        private void FixedUpdate()
        {
            MoveTowardsPlayer();
        }

        private void Movement()
        {

            transform.Translate(Vector2.down * _moveSpeed * Time.deltaTime);



            if (_canMoveTowards == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, _moveTowardsPlayerSpeed * Time.deltaTime);
            }

            if (transform.position.y < -6f)
            {
                Destroy(this.gameObject);
            }
        }



        private void MoveTowardsPlayer()
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, _playerInRadius);

            if (collider.CompareTag("Player"))
            {
                if(collider != null)
                {
                    _canMoveTowards = true;
                }

                
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _playerInRadius);
        }
    }
}

