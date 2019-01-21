using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AlertMeter : MonoBehaviour
{
    Image image;

    void Start ()
    {
        image = GetComponent<Image>();
    }

    void Update ()
    {
        image.fillAmount = SecurityManager.Instance.Alarm / 100;
    }
}
