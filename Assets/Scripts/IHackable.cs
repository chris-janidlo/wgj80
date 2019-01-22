using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class IHackable : MonoBehaviour
{
    public string HackName;

    public abstract HackableObject HackType { get; }
}

public enum HackableObject {
    Vent, Safe, Camera, Robot
}
