using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPoint : MonoBehaviour
{
    float maxTime, currentTime;
    new Light light;

    void Start ()
    {
        light = GetComponent<Light>();
    }


    void Update ()
    {
        light.range = currentTime / maxTime;
        currentTime -= Time.deltaTime;
    }

    public void Reveal (float time)
    {
        maxTime = time;
        currentTime = time;
    }
}
