using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TargetNamer : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public Image Background;

    public void SetName (string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            Text.text = "";
            Background.enabled = false;
        }
        else
        {
            Text.text = name;
            Background.enabled = true;
        }
    }
}
