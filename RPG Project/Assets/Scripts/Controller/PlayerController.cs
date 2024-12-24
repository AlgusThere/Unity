using RPG.Combat;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Controller
{
    public class PlayerController : MonoBehaviour
    {

        void Update()
        {
            InteractWithCombat();
            InteractWithMovement();
        }

        private void InteractWithMovement()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
                //lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            }
        }

        void InteractWithCombat()
        {
            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            foreach(RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                
                if(target == null)
                {
                    continue;
                }
                if(Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>().Attack(target);
                }
            }
        }

        private void MoveToCursor()
        {
            //Ray ray = GetMouseRay();
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                GetComponent<Mover>().MoveTo(hit.point);
                //MoveTo(hit.point);
            }
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}