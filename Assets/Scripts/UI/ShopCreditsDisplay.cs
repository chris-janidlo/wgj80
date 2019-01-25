﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ShopCreditsDisplay : MonoBehaviour
{
    TextMeshProUGUI text;

    void Start ()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update ()
    {
        text.text = "Your Credits: " + InventoryManager.Instance.Credits;
    }
}
