using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class SecurityManager : Singleton<SecurityManager>
{
    public float AlertTime;

    public bool Alert
    {
        get
        {
            return alertTimer > 0;
        }
        set
        {
            alertTimer = value ? AlertTime : 0;
        }
    }

    float alertTimer;

    void Start ()
    {
        SingletonSetInstance(this, true);
    }

    void Update ()
    {
        if (Alert)
        {
            Debug.Log("WEE WOO WEE WOO");
            alertTimer -= Time.deltaTime;
        }
    }
}
