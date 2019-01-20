using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class HackButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public TextMeshProUGUI TargetText;
    public Color NormalColor, HighlightedColor, PressedColor;
    // TODO:
    // public float FadeDuration;

    Action<Hack> onPointerEnterCallback, onPointerExitCallback, onClickCallback;
    Hack hack;

    bool clickable => hack.TargetObjectType == Scanner.Instance.Target?.HackType;
    bool hovered, pressed;

    void Update ()
    {
        if (!clickable)
        {
            TargetText.color = hovered ? HighlightedColor : NormalColor;
            pressed = false;
        }
    }

    public void Initialize (Hack hack, Action<Hack> onPointerEnterCallback, Action<Hack> onPointerExitCallback, Action<Hack> onClickCallback)
    {
        this.hack = hack;
        this.onPointerEnterCallback = onPointerEnterCallback;
        this.onPointerExitCallback = onPointerExitCallback;
        this.onClickCallback = onClickCallback;

        TargetText.text = hack.DisplayName;
    }

	public void OnPointerEnter (PointerEventData eventData)
	{
        hovered = true;
        TargetText.color = pressed ? PressedColor : HighlightedColor;
        onPointerEnterCallback(hack);
	}

	public void OnPointerExit (PointerEventData eventData)
	{
        hovered = false;
        TargetText.color = pressed ? HighlightedColor : NormalColor;
        onPointerExitCallback(hack);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
        if (!clickable) return;

        pressed = true;
        TargetText.color = PressedColor;
	}

	public void OnPointerUp(PointerEventData eventData)
	{
        if (!clickable) return;

        pressed = false;
        TargetText.color = NormalColor;

        if (hovered) onClickCallback(hack);
	}
}
