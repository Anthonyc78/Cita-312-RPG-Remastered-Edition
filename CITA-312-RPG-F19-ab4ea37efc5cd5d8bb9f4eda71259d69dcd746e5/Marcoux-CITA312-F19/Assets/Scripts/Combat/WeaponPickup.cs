using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player") // if a game object with the tag "Player" collides with the pickup 
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon); // than equip the weapon
                Destroy(gameObject); // destroy the pickup object
            }
        }
    }
}