using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class OverlayTriggerButton : MonoBehaviour
{
    public GameObject ToEnable;

    public Button OwnButton, ParentButton;

    void Start ()
    {
        OwnButton.onClick.AddListener(onClick);
    }

    void Update ()
    {
        OwnButton.interactable = !ToEnable.activeSelf;
        ParentButton.interactable = !ToEnable.activeSelf;
    }

    void onClick ()
    {
        ToEnable.SetActive(true);
    }
}
