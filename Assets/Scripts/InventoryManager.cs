using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class InventoryManager : Singleton<InventoryManager>
{
	[Range(1, 10)] // max of 10 since input currently uses number bar
	public int MaxHacks = 10;
	public List<Hack> AvailableHacks;
	
	void Awake ()
	{
		if (SingletonGetInstance() != null)
		{
			Destroy(gameObject);
		}
		else
		{
			SingletonSetInstance(this, false);
			transform.parent = null;
			DontDestroyOnLoad(gameObject);
		}
	}
}
