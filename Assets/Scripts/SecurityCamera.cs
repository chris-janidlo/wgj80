using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public float AlarmPerSecond;
    public Vector2 ShootTimeRange;
    public Laser LaserPrefab;
    public ParticleSystem RangeVisualizer;

    Quaternion initialRotation;
    bool playerInSight;
    float shootTimer;

    Vector3 oldPosition;

    void Start ()
    {
        initialRotation = transform.localRotation;
    }

    void Update ()
    {
        if (SecurityManager.Instance.Alert)
        {
            if (!playerInSight) return;
            
            transform.LookAt(DroneMovement.Instance.transform);
            transform.Rotate(-90, 0, 0);

            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0)
            {
                shootTimer = Random.Range(ShootTimeRange.x, ShootTimeRange.y);
                Instantiate(LaserPrefab, transform.position, transform.rotation * Quaternion.Euler(90, 0, 0));
            }
        }
        else
        {
            // TODO: animate this
            transform.localRotation = initialRotation;
        }

        var em = RangeVisualizer.emission;
        em.enabled = oldPosition == transform.position || SecurityManager.Instance.Alert;
        oldPosition = transform.position;
    }

    void OnTriggerEnter (Collider other)
    {
        Debug.Log(other);
        if (other.tag == "Player")
        {
            playerInSight = true;
        }
    }
    
    void OnTriggerStay (Collider other)
    {
        if (other.tag == "Player")
        {
            SecurityManager.Instance.Alarm += AlarmPerSecond * Time.deltaTime;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.tag == "Player")
        {
            playerInSight = false;
        }
    }
}
