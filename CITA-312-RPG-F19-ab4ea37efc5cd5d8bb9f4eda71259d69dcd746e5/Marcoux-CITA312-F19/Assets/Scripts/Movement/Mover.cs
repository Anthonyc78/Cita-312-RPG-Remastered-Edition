using RPG.Core;
using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;
using System.Collections.Generic;
using RPG.Resources;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] Transform target; // sets target position
        [SerializeField] float maxSpeed = 6f; // max speed of the player

        NavMeshAgent navMeshAgent; // sets the navmesh
        Health health; // sets the health

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>(); // gets the navmesh agent
            health = GetComponent<Health>(); // gets the health
        }

        void Update()
        {
            // disable navMeshAgent as soon as the AI is dead
            navMeshAgent.enabled = !health.IsDead();
            UpdateAnimator();
        } // Update

        public void StartMoveAction(Vector3 destination, float speedFraction)
        {
            GetComponent<ActionScheduler>().StartAction(this); // begins the action
            
            MoveTo(destination, speedFraction); // begins moving to target
        }



        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.destination = destination; // sets the navmesh location as the target
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction); // sets max speed using the navmesh
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true; // stops action
        }

        
        private void UpdateAnimator()
        {
            // get velocity from NavMeshAgent
            Vector3 velocity = navMeshAgent.velocity;
            // transform the direction to be a local velocity
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed); // gets the max speed
        }

        public object CaptureState() // used to save the game
        {
            Dictionary<string, object> data = new Dictionary<string, object>(); // creates a dictionary called 'data'
            data["position"] = new SerializableVector3(transform.position); // serializes the position of the player
            data["rotation"] = new SerializableVector3(transform.eulerAngles); // serializes the rotation of the player
            return data;
        }

        public void RestoreState(object state) // uses the CaptureState() to load the previous save
        {
            Dictionary<string, object> data = (Dictionary<string, object>)state; // calls the dictionary 'data'
            GetComponent<NavMeshAgent>().enabled = false; // disables the navmesh agent
            transform.position = ((SerializableVector3)data["position"]).ToVector(); // finds the position of the last save
            transform.eulerAngles = ((SerializableVector3)data["rotation"]).ToVector(); // finds the rotation of the last save
            GetComponent<NavMeshAgent>().enabled = true; // enables the navmesh agent
        }
    } // class Mover
} // namespace
