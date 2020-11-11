using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceDefender.Core
{
    public class ProjectileAudio : MonoBehaviour
    {
        private AudioSource _audioSource;


        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();

            if(_audioSource == null)
            {
                Debug.LogError("The AudioSource Is NULL");
            }

            _audioSource.Play();
        }
    }
}


