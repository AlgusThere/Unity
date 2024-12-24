using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {

        //[SerializeField] private Transform target;
        //Ray lastRay;

        void Update()
        {
            //if(Input.GetMouseButton(0))
            //{
            //    MoveToCursor();
            //    //lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            //}
            //Debug.DrawRay(lastRay.origin, lastRay.direction * 100, Color.black);
            //GetComponent<NavMeshAgent>().destination = target.position;

            UpdateAnimator();
        }

        public void MoveTo(Vector3 hit)
        {
            GetComponent<NavMeshAgent>().destination = hit;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}