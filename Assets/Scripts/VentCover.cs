using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentCover : MonoBehaviour
{
    public bool Unlocked;
    public float OpenDistance, OpenYOffset, OpenSpeed;

    bool shouldOpen => Unlocked && Vector3.Distance(DroneMovement.Instance.transform.position, transform.position) <= OpenDistance;

    IEnumerator Start ()
    {
        Vector3 startPos = transform.position;

        while (true)
        {
            if (shouldOpen)
            {
                while ((transform.position - startPos).y < OpenYOffset)
                {
                    transform.position += Vector3.up * OpenSpeed * Time.deltaTime;
                    yield return null;
                }
                transform.position = startPos + Vector3.up * OpenYOffset;
            }
            if (!shouldOpen) // NOT ELSE - bool may have changed since we last checked
            {
                while (transform.position.y > startPos.y)
                {
                    transform.position += Vector3.down * OpenSpeed * Time.deltaTime;
                    yield return null;
                }
                transform.position = startPos;
            }
        }
    }
}
