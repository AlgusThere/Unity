using RPG.Core;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {

        [SerializeField] float weaponRange;

        Transform targetObject;

        private void Update()
        {

            if (targetObject == null)
            {
                return;
            }
            if (GetIsInRange() == false)
            {
                GetComponent<Mover>().MoveTo(targetObject.position);
            }
            else
            {
                GetComponent<Mover>().Stop();
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, targetObject.position) < weaponRange;
        }

        public void Attack(CombatTarget target)
        {
            GetComponent<ActionScheduler>().StartAciton(this);
            //print("Saldýrý yapýldý.");
            targetObject = target.transform;
        }

        public void Cancel()
        {
            targetObject = null;
        }
    }
}
