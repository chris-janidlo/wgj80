using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene("Shop");
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
        Quaternion initialRotation = DoorTransform.localRotation, finalRotation = Quaternion.Euler(FinalRotation);

        while (amount <= 1)
        {
            DoorTransform.localRotation = Quaternion.Slerp(initialRotation, finalRotation, amount);
            amount += Time.deltaTime / OpenTime;
            yield return null;
        }
        DoorTransform.localRotation = finalRotation;

        open = true;
    }
}
