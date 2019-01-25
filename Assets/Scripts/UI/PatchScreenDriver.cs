using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PatchScreenDriver : MonoBehaviour
{
    public Transform HackContainer;
    public HackButton ButtonPrefab;
    public GameObject Overlay;
    public Button ContinueButton;
    public TextMeshProUGUI InfoBox;
    public string EmptyInfoString;
    public ShopInterface Shop;

    void Start ()
    {
        if (!InventoryManager.Instance.PatchesReady)
        {
            Destroy(gameObject);
            Shop.Initialize();
            return;
        }

        foreach (var hack in InventoryManager.Instance.HacksToBePatched)
        {
            var button = Instantiate(ButtonPrefab);
            button.Clickable = false;
            button.Initialize(hack, onPointerEnter, onPointerExit, h => {});
            button.transform.SetParent(HackContainer, false);
        }
        InventoryManager.Instance.PatchAll();

        Shop.Initialize();

        ContinueButton.onClick.AddListener(continueClick);
    }

    void Update ()
    {
        if (Overlay != null && Input.anyKeyDown)
        {
            Destroy(Overlay);
            Overlay = null;
        }
    }

    void onPointerEnter (Hack hack)
    {
        InfoBox.text = hack.InfoString();
    }

    void onPointerExit (Hack hack)
    {
        InfoBox.text = EmptyInfoString;
    }

    void continueClick ()
    {
        Destroy(gameObject);
    }
}
