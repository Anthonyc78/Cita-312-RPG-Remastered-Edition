using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using UnityEngine.Events;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f; // creates the max health
        [SerializeField] UnityEvent takeDamage; // creates a unity event that I can add audio to
        [SerializeField] UnityEvent onDie; // creates a unity event

        bool isDead = false;

        private void Start()
        {
            healthPoints = GetComponent<BaseStats>().GetHealth();
        }

        public bool IsDead()
        {
            return isDead; // checks if the health is at 0
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            if (healthPoints == 0)
            {
                onDie.Invoke(); // play the unity event
                Die(); // if the health is at 0 then it is dead
            }
            else
            {
                takeDamage.Invoke(); // play the unity event
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
