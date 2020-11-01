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

                PowerUpBehaviour behaviour = collision.transform.GetComponent<PowerUpBehaviour>();

                if (behaviour != null)
                {
                    UsePowerUp(behaviour);
                }

                
            }
        }

        private void UsePowerUp(PowerUpBehaviour behaviour)
        {
            switch (_powerUpType)
            {
                case powerUpType.TripleShot:
                    behaviour.UseTripleShot();
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

