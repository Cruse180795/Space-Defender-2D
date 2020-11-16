using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDefender.Core
{
    public class CameraShake : MonoBehaviour
    {

        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();

            if(_animator == null)
            {
                Debug.LogError("The Animator Is NULL");
            }
        }


        public void TriggerCameraShake()
        {
            _animator.SetTrigger("CanShake");
            
        }

    }
}


