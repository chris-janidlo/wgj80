using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using crass;

public class Scanner : Singleton<Scanner>
{
	public string InteractButton;

	public GameObject HackingHUD, AlreadyHackedHUD;
	public TargetNamer TargetNameDisplay;
	public float SpherecastRadius;
	public LayerMask HackableLayers;

	public IHackable Target { get; private set; } = null;

	void Awake ()
	{
		SingletonSetInstance(this, true);
	}

	void Update ()
	{
		RaycastHit hit;
		if (!Physics.SphereCast(transform.position, SpherecastRadius, transform.forward, out hit, Mathf.Infinity, HackableLayers, QueryTriggerInteraction.Collide))
		{
			HackingHUD.SetActive(false);
			AlreadyHackedHUD.SetActive(false);
			TargetNameDisplay.SetName("");
			Target = null;
			return;
		}

		Target = hit.collider.GetComponent<IHackable>();

		HackingHUD.SetActive(!Target.Hacked);
		AlreadyHackedHUD.SetActive(Target.Hacked);
		TargetNameDisplay.SetName(Target.HackName);

		if (Target.Hacked && Input.GetButtonDown(InteractButton))
		{
			Target.Interact();
		}
	}
}
