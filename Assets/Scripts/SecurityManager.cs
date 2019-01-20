using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class SecurityManager : Singleton<SecurityManager>
{
	[SerializeField, Tooltip("How alarmed the general system is in regards to the player. Ranges from 0-100; if it gets to 100, go into Alert mode")]
	float _alarm;
    public float Alarm
    {
        get
        {
            return _alarm;
        }
        set
        {
            if (value > _alarm)
            {
                alarmDecayDelayTimer = AlarmDecayDelay;
            }

            _alarm = Mathf.Clamp(value, 0, 100);
            
            if (_alarm == 100)
            {
                Alert = true;
            }
        }
    }

    [Tooltip("How much alarm is lost per second")]
    public float AlarmDecay;
    [Tooltip("How long in seconds until alarm starts decaying")]
    public float AlarmDecayDelay;

    [Tooltip("How long the system stays in Alert mode. As Alert state counts down, so does Alarm")]
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

    float alertTimer, alarmDecayDelayTimer;

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
            Alarm = (alertTimer / AlertTime) * 100;
        }
        else
        {
            alarmDecayDelayTimer -= Time.deltaTime;
            if (alarmDecayDelayTimer <= 0)
            {
                Alarm -= AlarmDecay * Time.deltaTime;
            }
        }
    }
}
