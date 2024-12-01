using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{

    NavMeshAgent navMeshAgent;
    public Transform targetPoint;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        navMeshAgent.SetDestination(targetPoint.position);
    }
}
