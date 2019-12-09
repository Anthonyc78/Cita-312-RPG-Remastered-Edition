using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)] // created a asset menu
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        [System.Serializable]
        class ProgressionCharacterClass // creating a progression chart for all character classes
        {
            [SerializeField] CharacterClass characterClass; // each character class
            [SerializeField] float[] health; // health
        }
    }
}