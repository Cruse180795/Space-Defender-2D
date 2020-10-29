using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

namespace SpaceDefender.Core
{
    public class ProjectileMover : MonoBehaviour
    {
        private enum UserType
        { 
            Player,
            Enemy
        }

        [Header("Projectile Config")]
        [SerializeField] private UserType _userType;
        [SerializeField] private float _projectileMoveSpeed = 7.5f;


        private void Update()
        {
            switch (_userType) 
            {
                case UserType.Player:
                    Movement(Vector3.up, _projectileMoveSpeed);
                    break;

                case UserType.Enemy:
                    Movement(Vector3.down, _projectileMoveSpeed);
                    break;
            }

        }

        private void Movement(Vector3 direction, float speed)
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }
}


