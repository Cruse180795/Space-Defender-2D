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
            AmmoRefill,
            HealthRegain,
            ShockWave,
            NegativeEffect
        }

        [SerializeField] private powerUpType _powerUpType;
        [SerializeField] private AudioClip _powerUpClip;
        [Range(0.1f, 1f)] [SerializeField] private float _clipVolume = 0.5f;
        [SerializeField] private float _collectionRange = 1.5f;


        private bool _inRange = false;
        private bool _canCollect = false;

        public bool CanCollect
        {
            get
            {
                return _canCollect;
            }
            set
            {
                _canCollect = value;
            }
        }


        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.C) && _inRange == true)
            {
                _canCollect = true;
            }
        }

        private void FixedUpdate()
        {
            QuickCollectPowerUp();
        }


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

                case powerUpType.HealthRegain:
                    behaviour.RegainHealth();
                    break;

                case powerUpType.ShockWave:
                    behaviour.UseShockWave();
                    break;

                case powerUpType.NegativeEffect:
                    behaviour.UseNegativeEffect();
                    break;

                default:
                    break;
            }
            AudioSource.PlayClipAtPoint(_powerUpClip, Camera.main.transform.position, _clipVolume);
            Destroy(this.gameObject);
        }


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _collectionRange);
        }

        private void QuickCollectPowerUp()
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, _collectionRange);

            if (collider.CompareTag("Player"))
            {
                Debug.Log("Player In Range");
                _inRange = true;
            }
        }

    }
}

