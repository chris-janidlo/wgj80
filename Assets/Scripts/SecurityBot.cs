using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SecurityBot : MonoBehaviour
{
    public List<Transform> PatrolPoints;
    public float StationaryTime;

    int targetPoint;
    NavMeshAgent agent;
    IEnumerator mainEnum;

    void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(mainEnum = main());
    }

    // Update is called once per frame
    void Update ()
    {
        if (SecurityManager.Instance.Alert)
        {
            agent.destination = DroneMovement.Instance.transform.position;
            if (mainEnum != null)
            {
                StopCoroutine(mainEnum);
                mainEnum = null;
            }
        }
        else if (mainEnum == null)
        {
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
