using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        void Update() // Update is called once per frame
        {
            if (!GetComponent<ParticleSystem>().IsAlive()) // if there is no particle system alive
            {
                Destroy(gameObject); // destroy the particle system
            }
        }
    }
}