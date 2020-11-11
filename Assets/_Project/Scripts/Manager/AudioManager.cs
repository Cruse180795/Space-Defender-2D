using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDefender.Manager
{
    public class AudioManager : MonoBehaviour
    {
        private void Awake()
        {
            SetUpSingleton();
        }


        private void SetUpSingleton()
        {
            if(FindObjectsOfType(GetType()).Length > 1)
            {
                Destroy(this.gameObject);
            }
            else
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }
    }
}

