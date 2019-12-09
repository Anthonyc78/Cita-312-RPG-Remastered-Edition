using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using RPG.Core;
using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicsControlRemover : MonoBehaviour
    {

        GameObject player; // creates a game object player

        private void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControl; // disable the players controls
            GetComponent<PlayableDirector>().stopped += EnableControl; // enable the players controls
        }

        void DisableControl(PlayableDirector pd)
        {
            GameObject player = GameObject.FindWithTag("Player"); // disables all actions for game objects with the tag "Player"
            player.GetComponent<ActionScheduler>().CancelCurrentAction(); // stop all actions
            player.GetComponent<PlayerController>().enabled = false; // disables the player controller
        }

        void EnableControl(PlayableDirector pd)
        {
            GameObject player = GameObject.FindWithTag("Player"); // enables all actions for game objects with the tag "Player"
            player.GetComponent<PlayerController>().enabled = true; // enables the player controller
        }
    }
}
