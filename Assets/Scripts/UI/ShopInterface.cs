using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class ShopInterface : MonoBehaviour
{
    public TextMeshProUGUI InfoBox;
    public string EmptyInfoBoxString;
    public Transform OwnedContainer;
    public Transform ShopContainer;
    public HackButton ShopButtonPrefab;

    List<Hack> shopList;

    Dictionary<Hack, Transform> buttonRefs;

    public void Initialize ()
    {
        InventoryManager.Instance.AppraiseInventory();
        // TODO: dynamic shop number
        shopList = InventoryManager.Instance.GetShopListing(10);

        buttonRefs = new Dictionary<Hack, Transform>();
        populateContainer(shopList, ShopContainer);
        populateContainer(InventoryManager.Instance.AvailableHacks, OwnedContainer);

        emptyInfoBox();
    }

    void populateContainer (List<Hack> hacks, Transform container)
    {
        foreach (Hack hack in hacks)
        {
            var button = Instantiate(ShopButtonPrefab);
            button.Initialize(hack, onButtonPointerEnter, onButtonPointerExit, onButtonClick);
            button.transform.SetParent(container, false);
            buttonRefs[hack] = button.transform;
        }
    }

    void emptyInfoBox ()
    {
        InfoBox.text = EmptyInfoBoxString;
    }

    void onButtonPointerEnter (Hack hack)
    {
        bool notBuyable = shopList.Contains(hack) && !InventoryManager.Instance.CanBuyHack(hack);
        bool notSellable = !shopList.Contains(hack) && hack.CurrentMarketPrice <= 0;
        
        string color = (notBuyable || notSellable) ? "red" : "green";
        string priceDisplay = notSellable ? "can't be sold" : hack.CurrentMarketPrice + " credits";

        InfoBox.text = hack.InfoString() + $"\n<color={color}>Value: {priceDisplay}</color>";
    }

    void onButtonPointerExit (Hack hack)
    {
        emptyInfoBox();
    }

    void onButtonClick (Hack hack)
    {
        if (shopList.Contains(hack))
        {
            if (InventoryManager.Instance.CanBuyHack(hack))
            {
                InventoryManager.Instance.BuyHack(hack);
                buttonRefs[hack].SetParent(OwnedContainer, false);
                shopList.Remove(hack);
            }
            else
            {
                Debug.Log("not enough money");
            }
        }
        else
        {
            if (hack.CurrentMarketPrice > 0)
            {
                InventoryManager.Instance.SellHack(hack);
                buttonRefs[hack].SetParent(ShopContainer, false);
                shopList.Add(hack);
            }
            else
            {
                Debug.Log("unsellable hack");
            }   
        }
    }
}
