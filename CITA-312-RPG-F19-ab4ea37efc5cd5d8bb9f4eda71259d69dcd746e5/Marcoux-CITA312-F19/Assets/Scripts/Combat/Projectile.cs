using RPG.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1; // used for the speed of the projectile
        [SerializeField] bool isHoming = true; // checkbox for if a projectile follows its target
        [SerializeField] GameObject hitEffect = null; // used for the hit impact effect
        Health target = null; // used the health to identify the target
        float damage = 0; // setting the damage of the weapon

        private void Start()
        {
            transform.LookAt(GetAimLocation()); // updates the targets location only when the projectile is fired
        }

        void Update() // Update is called once per frame
        {
            if (target == null) return;
            if (isHoming && !target.IsDead()) // checks if the projectile is homing and checks if the target is dead
            {
                transform.LookAt(GetAimLocation()); // updates the targets location constantly
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime); // location of target
        }

        public void SetTarget(Health target, float damage) // used to identify the target that the projectile is being guided towards
        {
            this.target = target; // sets target of projectile
            this.damage = damage; // sets damage of projectile
        }

        private Vector3 GetAimLocation() // aims the arrow/projectile at the target's center mass
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null)
            {
                return target.transform.position; // location of the target
            }
            return target.transform.position + Vector3.up * targetCapsule.height / 2; // center mass of the target
        }

        private void OnTriggerEnter(Collider other) // activates the damage from a projectile once collision happens
        {
            if (other.GetComponent<Health>() != target) return;
            if (target.IsDead()) return; // checks if target is dead before colliding with the box collider of the target
            target.TakeDamage(damage); // applies damage to target

            if (hitEffect != null)
            {
                Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }

            Destroy(gameObject); // destroys the projectile after impact
        }

    }
}

