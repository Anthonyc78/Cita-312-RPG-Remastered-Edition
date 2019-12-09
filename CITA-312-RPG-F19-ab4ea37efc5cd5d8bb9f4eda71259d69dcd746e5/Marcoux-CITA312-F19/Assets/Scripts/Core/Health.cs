using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.Core  
{
    public class Health : MonoBehaviour, ISaveable
    {
       [SerializeField] float healthPoints = 100f; // creates the max health

        bool isDead = false;

        public bool IsDead()
        {
            return isDead; // checks if the health is at 0
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if (healthPoints == 0)
            {
                Die(); // if the health is at 0 then it is dead
            }
        }

        private void Die()
        {
            if (isDead) return; // if the health is already 0 return dead

            isDead = true;
            
            GetComponent<Animator>().SetTrigger("die"); // start the death animation if isDead is returned
            GetComponent<ActionScheduler>().CancelCurrentAction(); // cancel all other actions
        }

        public object CaptureState()
        {
            return healthPoints; // save the health
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state; // load the health
        }
    } // class Health
} // namespace
