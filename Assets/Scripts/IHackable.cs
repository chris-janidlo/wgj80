using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class IHackable : MonoBehaviour
{
    public string HackName;

    public HackableObject HackType;

    public bool Hacked { get; protected set; }

    public void Hack ()
    {
        Hacked = true;
        Interact();
    }

    public abstract void Interact ();
}
