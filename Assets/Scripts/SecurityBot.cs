using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class SecurityBot : IHackable
{
    public float PlayerChaseDistance;
    public List<PatrolPoint> PatrolPoints;
    public float StationaryTime;
    public SecurityCamera ChildCam;

    int targetPoint;
    float oldStopDistance;
    NavMeshAgent agent;
    IEnumerator mainEnum;

	public override HackableObject HackType => HackableObject.Robot;

	void Start ()
    {
        agent = GetComponent<NavMeshAgent>();
        oldStopDistance = agent.stoppingDistance;
        ChildCam.HackName = HackName;
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

    public void ShowVisionConeHack (float time)
    {
        ChildCam.ShowVisionConeHack(time);
    }

    public void ShowPatrolPoints (float time)
    {
        foreach (var point in PatrolPoints)
        {
            point.Reveal(time);
        }
    }

    IEnumerator main ()
    {
        while (true)
        {
            agent.destination = PatrolPoints[targetPoint].transform.position;
            targetPoint = (targetPoint + 1) % PatrolPoints.Count;
            yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance <= Mathf.Epsilon);
            yield return new WaitForSeconds(StationaryTime);
        }
    }
}
