using RPG.Combat;
using RPG.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Controller
{
    public class PlayerController : MonoBehaviour
    {
        void Update()
        {
            if(InteractWithCombat() == true)
            {
                return;
            }
            if(InteractWithMovement() == true)
            {
                return;
            }
            //Debug.Log("Hiçbir þey yapýlmadý.");
        }

        //private bool InteractWithMovement()
        //{
        //    if (Input.GetMouseButton(0))
        //    {
        //        MoveToCursor();
        //        //lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    }
        //}

        private bool InteractWithCombat()
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
                return true;

            }
            return false;
        }

        private bool InteractWithMovement()
        {
            //Ray ray = GetMouseRay();
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if(Input.GetMouseButton(0))
                {
                    //Debug.Log("Hareket Edildi.");
                    GetComponent<Mover>().StartMoveAction(hit.point);
                }
                return true;

                //MoveTo(hit.point);
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }

}