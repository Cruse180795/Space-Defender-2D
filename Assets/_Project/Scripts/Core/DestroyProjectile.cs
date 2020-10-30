using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceDefender.Core
{
    public class DestroyProjectile : MonoBehaviour
    {
        private void Update()
        {
            DestroyObj();
        }


        private void DestroyObj()
        {
            if(transform.position.y > 6f || transform.position.y < -6f)
            {
                if(transform.parent != null)
                {
                    Destroy(transform.parent.gameObject);
                }

                Destroy(this.gameObject);
            }
        }
    }
}


