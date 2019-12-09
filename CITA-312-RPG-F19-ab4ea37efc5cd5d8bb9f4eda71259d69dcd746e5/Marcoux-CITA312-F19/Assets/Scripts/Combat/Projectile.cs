using RPG.Resources;
using UnityEngine;
using UnityEngine.Events;

namespace RPG.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed = 1; // used for the speed of the projectile
        [SerializeField] bool isHoming = true; // checkbox for if a projectile follows its target
        [SerializeField] GameObject hitEffect = null; // used for the hit impact effect
        [SerializeField] float maxLifeTime = 10; // max time a projectile can be on screen
        [SerializeField] GameObject[] destroyOnHit = null; // used to differentiate between game objects that we want to destroy on immediate impact
        [SerializeField] float lifeAfterImpact = 2; // the amount of time after impact before we destroy a specific object
        [SerializeField] UnityEvent onHit; // creates a unity event on Hit()

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

            Destroy(gameObject, maxLifeTime); // destroys the projectile after the max life time
        }

        private Vector3 GetAimLocation() // aims the arrow/projectile at the target's center mass
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>(); // sets the target to the capsule collider of the target
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

            speed = 0; // sets the speed of the project to 0 after impact

            onHit.Invoke();

            if (hitEffect != null)
            {
                Instantiate(hitEffect, GetAimLocation(), transform.rotation);
            }

            foreach (GameObject toDestroy in destroyOnHit) // destroys game objects that we specifically want to destroy immediatly on impact
            {
                Destroy(toDestroy); // destroys the objects here
            }

            Destroy(gameObject, lifeAfterImpact); // destroys the projectile after impact, after a specific amount of time
        }

    }
}

