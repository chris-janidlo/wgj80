using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using crass;

public class Scanner : Singleton<Scanner>
{
	public string UseButton;

	[Header("Spherecast Settings")]
	public float SpherecastRadius;
	public LayerMask HackRayLayers;
	public LayerMask HackableLayers;

	[Header("UI Settings")]
	public string InvalidTypeVT;
	public string OutOfRangeVT, TotallyValidVT, 
	              OpenHackMenuPrompt, UseHackPrompt;
	public Color ValidColor, InvalidColor;

	[Header("UI References")]
	public GameObject HackingUIContainer;
	public TextMeshProUGUI EquippedHackDisplay, TargetNameDisplay, ValidityText, SubPrompt;

	public Hack ActiveHack { get; set; }

	void Awake ()
	{
		SingletonSetInstance(this, true);
	}

	void Start ()
	{
		ActiveHack = InventoryManager.Instance.AvailableHacks[0];
	}

	void Update ()
	{
		EquippedHackDisplay.text = ActiveHack.DisplayName;

		RaycastHit hit;
		bool didHit = Physics.SphereCast(transform.position, SpherecastRadius, transform.forward, out hit, Mathf.Infinity, HackRayLayers, QueryTriggerInteraction.Ignore);

		bool hackableTarget = didHit &&
			((1 << hit.collider.gameObject.layer) & HackableLayers) != 0; // layer is in hackable layers

		HackingUIContainer.SetActive(hackableTarget);
		if (!hackableTarget) return;

		IHackable target = hit.collider.GetComponentInParent<IHackable>();

		TargetNameDisplay.text = target.HackName;

		// assume target is invalid, do things that are shared in every invalid case:
		ValidityText.color = InvalidColor;
		SubPrompt.text = OpenHackMenuPrompt;

		// figure out why:
		if (ActiveHack.TargetObjectType != target.HackType)
		{
			ValidityText.text = InvalidTypeVT;
		}
		else if (!ActiveHack.InfiniteRange && Vector3.Distance(DroneMovement.Instance.Position, target.transform.position) > ActiveHack.Range)
		{
			ValidityText.text = OutOfRangeVT;
		}
		// if we assumed wrong:
		else
		{
			ValidityText.text = TotallyValidVT;
			ValidityText.color = ValidColor;

			SubPrompt.text = UseHackPrompt;
		}

		if (Input.GetButtonDown(UseButton))
		{
			ActiveHack.UseOn(target);
		}
	}
}
