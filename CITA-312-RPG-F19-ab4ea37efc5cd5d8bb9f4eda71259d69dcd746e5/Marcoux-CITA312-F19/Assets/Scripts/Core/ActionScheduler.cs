using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;

        public void StartAction(IAction action)
        {
            if (currentAction == action) return; // if there is already an action continue until there isnt one
            
            if (currentAction != null) // if there is a current action cancel it
            {
                currentAction.Cancel(); // cancel the action
            }
            currentAction = action; // begin the action
        }

        public void CancelCurrentAction()
        {
            StartAction(null); // stops the current action
        }
    }
}
