using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float timeBetweenAttacks = 1f; // amount of time between attacks
        [SerializeField] Transform rightHandTransform = null; // position of the right hand
        [SerializeField] Transform leftHandTransform = null; // position of the left hand
        [SerializeField] Weapon defaultWeapon = null; // sets the default weapon that a player or enemy has
        [SerializeField] string defaultWeaponName = "Unarmed"; // sets the default weapon name to 'Unarmed'

        Health target; // sets the target with the health script
        float timeSinceLastAttack = Mathf.Infinity; // cooldown timer between attacks
        Weapon currentWeapon = null; // sets current weapon to null

        private void Start() // equips a weapon on start
        {
            Weapon weapon = Resources.Load<Weapon>(defaultWeaponName);
            EquipWeapon(defaultWeapon);
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime; // starts count on cooldown between attacks

            if (target == null) return; // checking if there is an enemy
                                        // if there is check if he is dead
            if (target.IsDead()) return; // stop attacking the target if they are dead
                                        // if the enemy is not dead is he in range
            if (!GetIsInRange()) // move only if a target is NOT within range
            {
                GetComponent<Mover>().MoveTo(target.transform.position, 1f); // move to target only if they are not in range
            }
            else // in range, attack!
            {
                GetComponent<Mover>().Cancel(); // stop moving
                AttackBehavior(); // attack
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon; // current weapon is the one in your hand
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform, leftHandTransform, animator); // spawn the weapon in the correct hand
        }

        private void AttackBehavior()
        {
            // look at our traget 
            transform.LookAt(target.transform);

            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                // This will trigger the Hit() event
                TriggerAttack();
                timeSinceLastAttack = 0f; // resets the attack cooldown
            }

        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack"); // reset rest animation
            GetComponent<Animator>().SetTrigger("attack"); // begin attack animation
        }

        // this is an animation event
        void Hit()
        {
            if (target == null)
            {
                return; // attack the target
            }
            if (currentWeapon.HasProjectile()) // if your current weapon is a projectile weapon like a bow or fireball
            {
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target); // shoot the projectile
            }
            else
            {
                target.TakeDamage(currentWeapon.GetDamage()); // damage is dealt to target
            }
        }

        void Shoot()
        {
            Hit(); // uses Hit() instead of creating another void for shoot that does the same thing as Hit()
        }

        private bool GetIsInRange() //checks range of target
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetRange(); // checks to make sure the distance between you and your target is less than the max range of your weapon
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null)
            {
                return false;
            }
            Health targetToTest = combatTarget.GetComponent<Health>(); // checks if what you clicked is a target
            return targetToTest != null && !targetToTest.IsDead(); // returns the targetToTest if !null and the target is dead
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this); // starts attack
            target = combatTarget.GetComponent<Health>(); // identifies the target with the Health script
        }

        public void Cancel() // used to cancel animations and actions
        {
            StopAttack(); // stops attacking animation and action
            target = null; // sets the target to null
            GetComponent<Mover>().Cancel(); // cancels movement
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack"); // reset attack animation
            GetComponent<Animator>().SetTrigger("stopAttack"); // begin rest animation
        }
    } // class Fighter
} // namespace
