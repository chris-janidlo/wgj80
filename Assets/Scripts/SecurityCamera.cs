using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    public float AlarmPerSecond;
    public Vector2 ShootTimeRange;
    public Laser LaserPrefab;

    Quaternion initialRotation;
    bool playerInSight;
    float shootTimer;

    void Start ()
    {
        initialRotation = transform.rotation;
    }

    void Update ()
    {
        if (SecurityManager.Instance.Alert)
        {
            if (!playerInSight) return;
            
            transform.LookAt(DroneMovement.Instance.transform);
            transform.Rotate(90, 0, 0);

            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0)
            {
                shootTimer = Random.Range(ShootTimeRange.x, ShootTimeRange.y);
                Instantiate(LaserPrefab, transform.position, transform.rotation);
            }
        }
        else
        {
            // TODO: animate this
            transform.rotation = initialRotation;
        }
    }

    void OnTriggerEnter (Collider other)
    {
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
