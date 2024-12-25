using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        MonoBehaviour currentAction;

        public void StartAciton(MonoBehaviour action)
        {
            if(currentAction == action)
            {
                return;
            }

            if(currentAction != null)
            {
                Debug.Log("Ýptal edildi." + currentAction);
            }


            currentAction = action;
        }
    }
}
