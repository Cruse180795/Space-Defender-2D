using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDefender.Enemy
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private float _enemyMoveSpeed = 5f;



        private void Update()
        {
            Movement();
        }


        private void Movement()
        {
            transform.Translate(Vector3.down * _enemyMoveSpeed * Time.deltaTime);

            if(transform.position.y < -6f)
            {
                transform.position = new Vector3(Random.Range(-8f, 8f), 8f, 0f);
            }
        }
    }
}



