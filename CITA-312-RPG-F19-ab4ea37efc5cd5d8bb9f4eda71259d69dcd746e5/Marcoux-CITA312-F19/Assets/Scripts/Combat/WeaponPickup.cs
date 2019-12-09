using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null; // the weapon can now be called by name using this
        [SerializeField] float respawnTime = 5; // pickup respawn timer

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player") // if a game object with the tag "Player" collides with the pickup 
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon); // than equip the weapon
                StartCoroutine(HideForSeconds(respawnTime)); // call the couroutine to start respawn timer
            }
        }

        private IEnumerator HideForSeconds(float seconds) // respawn coroutine
        {
            ShowPickup(false); // disables the collider and the child
            yield return new WaitForSeconds(seconds);
            ShowPickup(true); // enables the collider and the child
        }

        private void ShowPickup(bool shouldShow)
        {
            GetComponent<Collider>().enabled = shouldShow;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow); // disables and enables the collider and child to have the pickups disappear for 5 seconds and then reappear
            }
        }
    }
}