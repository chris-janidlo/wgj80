using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : IHackable
{
    public VentCover[] Covers;

	public override HackableObject HackType => HackableObject.Vent;

    float openTime;

    void Update ()
    {
        openTime -= Time.deltaTime;
        foreach (var c in Covers)
        {
            c.Unlocked = openTime >= 0;
        }
    }

    public void OpenHack (float time)
    {
        openTime = time;
    }
}
