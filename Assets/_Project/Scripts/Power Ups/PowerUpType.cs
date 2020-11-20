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
            ShieldBoost,
            AmmoRefill
        }

        [SerializeField] private powerUpType _powerUpType;
        [SerializeField] private AudioClip _powerUpClip;
        [Range(0.1f, 1f)] [SerializeField] private float _clipVolume = 0.5f;


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
                case powerUpType.ShieldBoost:
                    behaviour.UseShieldBoost();
                    break;

                case powerUpType.AmmoRefill:
                    behaviour.RefillAmmo();
                    break;
                default:
                    break;
            }
            AudioSource.PlayClipAtPoint(_powerUpClip, Camera.main.transform.position, _clipVolume);
            Destroy(this.gameObject);
        }
    }
}

