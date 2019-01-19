using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using crass;

public class HackingControls : MonoBehaviour
{
	public string InteractButton;

	public GameObject HackingHUD, AlreadyHackedHUD;
	public TargetNamer TargetNameDisplay;
	public float SpherecastRadius;
	public LayerMask HackableLayers;

	IHackable target;

	void Update ()
	{

		RaycastHit hit;
		if (!Physics.SphereCast(transform.position, SpherecastRadius, transform.forward, out hit, Mathf.Infinity, HackableLayers, QueryTriggerInteraction.Collide))
		{
			HackingHUD.SetActive(false);
			AlreadyHackedHUD.SetActive(false);
			TargetNameDisplay.SetName("");
			return;
		}

		target = hit.collider.GetComponent<IHackable>();

		HackingHUD.SetActive(!target.Hacked);
		AlreadyHackedHUD.SetActive(target.Hacked);
		TargetNameDisplay.SetName(target.HackName);

		if (target.Hacked)
		{
			if (Input.GetButtonDown(InteractButton))
			{
				target.Interact();
			}
		}
		else
		{
			tryToHack();
		}
	}

	void tryToHack ()
	{
		Hack hackToUse = null;

		int mh = InventoryManager.Instance.AvailableHacks.Count;
		// check top down so that we prioritize lower-valued keys, in the case that multiple are currently down
		for (int i = mh; i > 0; i--)
		{
			if (Input.GetKeyDown((i % mh).ToString()))
			{
				hackToUse = InventoryManager.Instance.AvailableHacks[i - 1];
				if (hackToUse.TargetObjectType != target.HackType) hackToUse = null;
			}
		}

		if (hackToUse == null) return;

		bool caught = RandomExtra.Chance(hackToUse.RevealChance);
		bool patched = RandomExtra.Chance(hackToUse.PatchChance);

		if (caught)
		{
			SecurityManager.Instance.Alert = true;
		}
		if (patched)
		{
			// TODO: set some persistent state
			Debug.Log("you got patched son");
		}

		target.Hack();
	}
}
