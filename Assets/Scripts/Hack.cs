using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Hack", menuName = "Hack")]
public class Hack : ScriptableObject
{
    public string DisplayName;
    [Tooltip("Either some godawful 1337sp34k or some made-up attack vector")]
    public string FlavorText;
    public HackableObject TargetObjectType;
    [Range(0, 1)]
    public float RevealChance, PatchChance;
    public int Range;
    public bool InfiniteRange;
}

public enum HackableObject {
    Vent, Safe, Camera, Robot
}
