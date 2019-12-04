using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        { // used to identify different portals in the same scene
            A, B, C, D, E
        }

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 0.5f; // how long the fade is in frames
        [SerializeField] float fadeInTime = 1.5f; // how long the fade is in frames
        [SerializeField] float fadeWaitTime = 0.5f; // how long the wait is in frames

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if (sceneToLoad < 0)
            {
                Debug.LogError("Scene to load not set."); // if the portal doesnt have a scene assigned to it this message pops up
                yield break;
            }

            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();

            yield return fader.FadeOut(fadeOutTime); // fade out over a series of frames

            SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
            wrapper.Save(); // save current level

            yield return SceneManager.LoadSceneAsync(sceneToLoad); // load over a series of frames

            wrapper.Load(); // load current level

            Portal otherPortal = GetOtherPortal(); // update the player
            UpdatePlayer(otherPortal);

            yield return new WaitForSeconds(fadeWaitTime); // wait for a series of frames for the camera to stablelize
            yield return fader.FadeIn(fadeInTime); // fade in over a series of frames

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.rotation = otherPortal.spawnPoint.rotation; // used to find the location of the player and sets their spawn point coming through a portal
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;

                return portal;
            }

            return null;
        }
    }

}