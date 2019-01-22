using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using crass;

[CreateAssetMenu(fileName = "Hack", menuName = "Hack")]
public class Hack : ScriptableObject
{
    public string DisplayName;
    [Tooltip("Either some godawful 1337sp34k or some made-up attack vector")]
    public string FlavorText;
    public string EffectText;
    public HackableObject TargetObjectType;
    public string HackMethodName;
    public float HackMethodParam;
    public bool PassParam;
    [Range(0, 1)]
    public float RevealChance, PatchChance;
    public int Range;
    public bool InfiniteRange;
    public Vector2Int MarketPriceRange;
    public int CurrentMarketPrice;

    public void UseOn (IHackable target)
    {
        MethodInfo hackMethod = target.GetType().GetMethod(HackMethodName);
        hackMethod.Invoke(target, PassParam ? new object[] {HackMethodParam} : null);

        bool caught = RandomExtra.Chance(RevealChance);
		bool patched = RandomExtra.Chance(PatchChance);

		if (caught)
		{
			SecurityManager.Instance.Alert = true;
		}
		if (patched)
		{
            InventoryManager.Instance.HacksToBePatched.Add(this);
		}
    }

    public int NewMarketPrice ()
    {
        return CurrentMarketPrice = Random.Range(MarketPriceRange.x, MarketPriceRange.y);
    }
}
