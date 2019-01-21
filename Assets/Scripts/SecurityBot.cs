using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SecurityBot : MonoBehaviour
{
    public float PlayerChaseDistance;
    public List<Transform> PatrolPoints;
    public float StationaryTime;

    int targetPoint;
    float oldStopDistance;
    NavMeshAgent agent;
    IEnumerator mainEnum;

    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        oldStopDistance = agent.stoppingDistance;
        StartCoroutine(mainEnum = main());
    }

    // Update is called once per frame
    void Update ()
    {
        if (SecurityManager.Instance.Alert)
        {
            agent.destination = DroneMovement.Instance.transform.position;
            if (mainEnum != null) // first frame of alert
            {
                StopCoroutine(mainEnum);
                mainEnum = null;

                oldStopDistance = agent.stoppingDistance;
                agent.stoppingDistance = PlayerChaseDistance;
            }
        }
        else if (mainEnum == null)
        {
            agent.stoppingDistance = oldStopDistance;
            StartCoroutine(mainEnum = main());
        }
    }

    IEnumerator main ()
    {
        while (true)
        {
            agent.destination = PatrolPoints[targetPoint].position;
            targetPoint = (targetPoint + 1) % PatrolPoints.Count;
            yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance <= Mathf.Epsilon);
            yield return new WaitForSeconds(StationaryTime);
        }
    }
}
