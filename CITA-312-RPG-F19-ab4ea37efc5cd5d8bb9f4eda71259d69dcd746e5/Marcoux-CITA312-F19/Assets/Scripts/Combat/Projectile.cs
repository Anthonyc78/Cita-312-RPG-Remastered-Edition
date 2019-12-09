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
        Health target = null; // used the health to identify the target
        float damage = 0; // setting the damage of the weapon

        void Update() // Update is called once per frame
        {
            if (target == null) return;

            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(Health target, float damage) // used to identify the target that the projectile is being guided towards
        {
            this.target = target;
            this.damage = damage;
        }

        private Vector3 GetAimLocation() // aims the arrow/projectile at the target's center mass
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null)
            {
                return target.transform.position;
            }
            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target) return;
            target.TakeDamage(damage);
            Destroy(gameObject);
        }

    }
}

