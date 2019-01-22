using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : IHackable
{
    public int Credits;
    public float OpenTime;
    public Vector3 FinalRotation;
    public Transform DoorTransform;

	public override HackableObject HackType => HackableObject.Safe;

	bool unlocked, open;

    void OnTriggerEnter (Collider other)
    {
        if (open && other.tag == "Player")
        {
            InventoryManager.Instance.Credits += Credits;
            // load shopping scene
        }
    }

    public void OpenHack ()
    {
        if (!unlocked)
        {
            unlocked = true;
            StartCoroutine(openHackRoutine());
        }
    }

    IEnumerator openHackRoutine ()
    {
        float amount = 0;
        Quaternion initialRotation = DoorTransform.rotation, finalRotation = Quaternion.Euler(FinalRotation);

        while (amount <= 0)
        {
            DoorTransform.rotation = Quaternion.Slerp(initialRotation, finalRotation, amount);
            amount += Time.deltaTime / OpenTime;
            yield return null;
        }
        DoorTransform.rotation = finalRotation;

        open = true;
    }
}
