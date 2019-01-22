using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

public class InventoryManager : Singleton<InventoryManager>
{
	[Range(1, 10)] // max of 10 since input currently uses number bar
	public int MaxHacks = 10;
	public List<Hack> AvailableHacks, BuyableHacks;
	public HashSet<Hack> HacksToBePatched;
	public int Credits;
	public Dictionary<HackableObject, Vector2Int> NumberRangePerHackTypeInShop;
	
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

	public void PatchAll ()
	{
		foreach (var hack in HacksToBePatched)
		{
			AvailableHacks.Remove(hack);
		}
		HacksToBePatched.Clear();
	}

	public Dictionary<HackableObject, List<Hack>> GetShopListing ()
	{
		ILookup<HackableObject, Hack> inLookup = BuyableHacks.ToLookup(x => x.TargetObjectType); // basically a dictionary, groups hacks by target type
		Dictionary<HackableObject, List<Hack>> outDict = new Dictionary<HackableObject, List<Hack>>();
		
		foreach (var pair in NumberRangePerHackTypeInShop)
		{
			outDict[pair.Key] =
				inLookup[pair.Key]
				.OrderBy(x => System.Guid.NewGuid())
				.Take(Random.Range(pair.Value.x, pair.Value.y))
				.ToList();

			foreach (var hack in outDict[pair.Key])
			{
				hack.NewMarketPrice();
			}
		}

		return outDict;
	}

	// assumes we have enough credits
	public void BuyHack (Hack hack)
	{
		Credits -= hack.CurrentMarketPrice;
		BuyableHacks.Remove(hack);
		AvailableHacks.Add(hack);
	}
}
