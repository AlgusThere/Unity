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
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
                //lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            }
        }

        private void MoveToCursor()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit)
            {
                GetComponent<Mover>().MoveTo(hit.point);
                //MoveTo(hit.point);
            }
        }
    }

}