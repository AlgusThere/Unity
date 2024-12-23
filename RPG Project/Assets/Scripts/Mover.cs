using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{

    [SerializeField] private Transform target;

    Ray lastRay;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            MoveToCursor();
            //lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        }
        //Debug.DrawRay(lastRay.origin, lastRay.direction * 100, Color.black);
        //GetComponent<NavMeshAgent>().destination = target.position;
    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        bool hasHit = Physics.Raycast(ray, out hit);
        if(hasHit)
        {
            GetComponent<NavMeshAgent>().destination = hit.point;
        }
    }
}
