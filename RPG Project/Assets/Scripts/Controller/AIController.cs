using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Controller
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspictionTime = 5f;
        [SerializeField] float wayPointTolerence = 1.1f;
        [SerializeField] float wayPointLifeTime = 3f;

        [SerializeField] PatrolPath patrolPath;

        GameObject player;
        Fighter fighter;
        Health health;
        Mover mover;

        Vector3 enemyLocation;
        float timeSinceLastSawPlayer;
        float timeSinceArrivedWaypoint;
        int currentWayPointIndex = 0;

        void Start()
        {
            player = GameObject.FindWithTag("Player");
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            enemyLocation = transform.position;
        }

        void Update()
        {
            if(health.IsDead())
            {
                return;
            }

            if(DistanceToPlayer() < chaseDistance && fighter.CanAttack(player))
            {
                timeSinceLastSawPlayer = 0;
                //print(player.name + "Takip edilmeli.");
                fighter.Attack(player);
            }
            else if(timeSinceLastSawPlayer < suspictionTime)
            {
                GetComponent<ActionScheduler>().CancelCurrentAction();
            }
            else
            {
                Vector3 nextPosition = enemyLocation;

                if(patrolPath != null)
                {
                    if(AtWayPoint())
                    {
                        timeSinceArrivedWaypoint = 0;
                        CycleWayPoint();
                    }

                    nextPosition = GetNextWayPoint();
                    //Debug.Log(currentWayPointIndex);
                }
                //fighter.Cancel();
                if(timeSinceArrivedWaypoint > wayPointLifeTime)
                {
                    mover.StartMoveAction(nextPosition);
                }
            }

            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedWaypoint += Time.deltaTime;
        }

        private Vector3 GetNextWayPoint()
        {
            return patrolPath.GetWayPointPosition(currentWayPointIndex);
        }

        private void CycleWayPoint()
        {
            currentWayPointIndex = patrolPath.GetNextIndex(currentWayPointIndex);
        }

        private bool AtWayPoint()
        {
            float distanceWayPoint = Vector3.Distance(transform.position, GetNextWayPoint());
            return distanceWayPoint < wayPointTolerence;
        }

        private float DistanceToPlayer()
        {
            return Vector3.Distance(player.transform.position, transform.position);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
