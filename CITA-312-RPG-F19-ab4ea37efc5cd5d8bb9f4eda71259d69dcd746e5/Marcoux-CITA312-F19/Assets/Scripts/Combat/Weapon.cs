using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOverride = null; // sets animator over ride controller to null
        [SerializeField] GameObject equippedPrefab = null; // sets equipped weapon to null
        [SerializeField] float weaponDamage = 5f; // sets base weapon damage
        [SerializeField] float weaponRange = 2f; // sets base weapon range
        [SerializeField] bool isRightHanded = true; // creates a check box for which hand the weapon will be used with
        [SerializeField] Projectile projectile = null; // sets the projectile to null

        const string weaponName = "Weapon"; // creates a way to identify weapons by a name

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator) // used to equip weapon pickups
        {
            DestroyOldWeapon(rightHand, leftHand); // destroys old weapon/the weapon in your hand

            if (equippedPrefab != null) // if there is a equipped weapon
            {
                Transform handTransform = GetTransform(rightHand, leftHand); // find the location of the left hand and right hand
                GameObject weapon = Instantiate(equippedPrefab, handTransform); // spawn the weapon in the correct hand position
                weapon.name = weaponName; // give the weapon a name
            }

            if (animatorOverride != null) // check if there is a animator over ride controller selected
            {
                animator.runtimeAnimatorController = animatorOverride; // if there is use it
            }
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand) // destroy the old weapon
        {
            Transform oldWeapon = rightHand.Find(weaponName); // the location of the weapon = right hand and the weapon name
            if (oldWeapon == null) // if there is no old weapon in the right hand check the left hand
            {
                oldWeapon = leftHand.Find(weaponName); // the weapon = left hand and the weapon name
            }
            if (oldWeapon == null) return;

            oldWeapon.name = "DESTROYING"; // set the old weapons name to destroying so its easier to tell what it is in the hierarchy while testing
            Destroy(oldWeapon.gameObject); // destroy the old weapon
        }

        private Transform GetTransform(Transform rightHand, Transform leftHand) // find the location of the hand that is supposed to hold the weapon
        {
            Transform handTransform; // handTransform refers to the location of the hand holding the weapon
            if (isRightHanded) handTransform = rightHand; // if the check box for isRightHanded is checked the location of handTransform = rightHand
            else handTransform = leftHand; // if the check box for isRightHanded is not checked the location of handTransform = leftHand
            return handTransform; // return the location of the hand
        }

        public bool HasProjectile() // does the weapon have a projectile
        {
            return projectile != null; // return projectile if it does have a projectile
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target) // shoot the prjectile
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity); // spawn the projectile in the left or right hand with the proper rotation
            projectileInstance.SetTarget(target, weaponDamage); // set the target for the projectile to hit

        }

        public float GetDamage() // get the damage for the weapon
        {
            return weaponDamage; // return the damage for the weapon
        }

        public float GetRange() // get the range for the weapon
        {
            return weaponRange; // return the range for the weapon
        }
    }
}