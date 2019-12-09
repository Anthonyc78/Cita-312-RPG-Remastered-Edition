using UnityEngine;
using RPG.Resources;

namespace RPG.Combat
{
    // this ensures that "Health" is always included in a combat target
    [RequireComponent(typeof(Health))] 
    public class CombatTarget : MonoBehaviour
    {

    }

}