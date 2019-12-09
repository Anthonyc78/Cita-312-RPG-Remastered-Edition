using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save"; // sets the default save file
        [SerializeField] float fadeInTime = 1.2f; // fade in time

        IEnumerator Start()
        {
            Fader fader = FindObjectOfType<Fader>(); // sets the fader
            fader.FadeOutImmediate(); // fade out
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile); // loads the last save
            yield return fader.FadeIn(fadeInTime); // fade in
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load(); // loads the game when 'l' is pressed
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                Save(); // saves the game when 's' is pressed
            }
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile); // call to saving system save
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile); // call to saving system load
        }
    }

}