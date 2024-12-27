using RPG.Combat;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {

        //[SerializeField] private Transform target;
        //Ray lastRay;

        NavMeshAgent navMeshAgent;
        Health health;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            navMeshAgent.enabled = !health.IsDead();
            
            //if(Input.GetMouseButton(0))
            //{
            //    MoveToCursor();
            //    //lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            //}
            //Debug.DrawRay(lastRay.origin, lastRay.direction * 100, Color.black);
            //GetComponent<NavMeshAgent>().destination = target.position;

            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 hit)
        {
            GetComponent<ActionScheduler>().StartAciton(this);
            GetComponent<Fighter>().Cancel();
            navMeshAgent.destination = hit;
            navMeshAgent.isStopped = false;
        }

        public void MoveTo(Vector3 hit)
        {
            navMeshAgent.destination = hit;
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}