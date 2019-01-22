using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecurityCamera : IHackable
{
    public float AlarmPerSecond;
    public Vector2 ShootTimeRange;
    public bool IsRobot;
    public Laser LaserPrefab;
    public ParticleSystem RangeVisualizer;
    public Image DisabledIndicator;
    public Collider[] ParentColliders;

    Quaternion initialRotation;
    bool playerInSight, newlyAlert = true;
    float shootTimer, showConeTimer, disabledTimer;

    bool disabled => disabledTimer >= 0;

	public override HackableObject HackType => IsRobot ? HackableObject.Robot : HackableObject.Camera;

	void Start ()
    {
        initialRotation = transform.localRotation;
    }

    void Update ()
    {
        showConeTimer -= Time.deltaTime;

        var em = RangeVisualizer.emission;
        em.enabled = showConeTimer >= 0 || SecurityManager.Instance.Alert;

        DisabledIndicator.enabled = disabled;
        disabledTimer -= Time.deltaTime;
        if (disabled) return;

        if (SecurityManager.Instance.Alert)
        {
            if (newlyAlert)
            {
                newlyAlert = false;
                shootTimer = Random.Range(ShootTimeRange.x, ShootTimeRange.y);
            }
            
            transform.LookAt(DroneMovement.Instance.transform);
            transform.Rotate(-90, 0, 0);
            
            if (!playerInSight) return;

            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0)
            {
                shootTimer = Random.Range(ShootTimeRange.x, ShootTimeRange.y);
                var laser = Instantiate(LaserPrefab, transform.position, transform.rotation * Quaternion.Euler(90, 0, 0));
                foreach (var col in GetComponentsInChildren<Collider>().Concat(ParentColliders))
                {
                    Physics.IgnoreCollision(col, laser.GetComponent<Collider>());
                }
            }
        }
        else
        {
            // TODO: animate this
            transform.localRotation = initialRotation;
            newlyAlert = true;
        }
    }

    void OnTriggerEnter (Collider other)
    {
        if (disabled) return;

        if (other.tag == "Player")
        {
            playerInSight = true;
        }
    }
    
    void OnTriggerStay (Collider other)
    {
        if (disabled) return;

        if (other.tag == "Player")
        {
            SecurityManager.Instance.Alarm += AlarmPerSecond * Time.deltaTime;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (disabled) return;

        if (other.tag == "Player")
        {
            playerInSight = false;
        }
    }

    public void ShowVisionConeHack (float time)
    {
        showConeTimer = time;
    }

    public void DisableHack (float time)
    {
        disabledTimer = time;
        playerInSight = false;
    }
}
