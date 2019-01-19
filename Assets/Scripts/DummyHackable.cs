using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyHackable : IHackable
{
	public override void Interact ()
	{
        Debug.Log("interacted with");
	}
}
