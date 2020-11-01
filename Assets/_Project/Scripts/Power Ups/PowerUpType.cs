using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDefender.PowerUps
{
    public class PowerUpType : MonoBehaviour
    {
        private enum powerUpType
        {
            TripleShot,
            SpeedBoost,
            ShieldBoost
        }

        [SerializeField] private powerUpType _powerUpType;



        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                switch (_powerUpType)
                {
                    case powerUpType.TripleShot:
                        Debug.Log("Power Up Collected: " + gameObject.name);
                        break;
                    case powerUpType.SpeedBoost:
                        Debug.Log("Power Up Collected: " + gameObject.name);
                        break;
                    case powerUpType.ShieldBoost:
                        Debug.Log("Power Up Collected: " + gameObject.name);
                        break;
                    default:
                        break;
                }

                Destroy(this.gameObject);
            }
        }
    }
}

