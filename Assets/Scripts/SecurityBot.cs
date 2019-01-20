using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SecurityBot : MonoBehaviour
{
    public List<Transform> PatrolPoints;

    int targetPoint;
    NavMeshAgent agent;

    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        goToNextPoint();
    }

    // Update is called once per frame
    void Update ()
    {
        if (SecurityManager.Instance.Alert)
        {
            agent.destination = DroneMovement.Instance.transform.position;
        }
        else if (!agent.pathPending && agent.remainingDistance <= Mathf.Epsilon)
        {
            goToNextPoint();
        }
    }

    void goToNextPoint ()
    {
        agent.destination = PatrolPoints[targetPoint].position;
        targetPoint = (targetPoint + 1) % PatrolPoints.Count;
    }
}
