using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currentAction;

        public void StartAciton(IAction action)
        {
            if(currentAction == action)
            {
                return;
            }

            if(currentAction != null)
            {
                //Debug.Log("Ýptal edildi." + currentAction);
                currentAction.Cancel();
            }


            currentAction = action;
        }
    }
}
