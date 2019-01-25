using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopLeaveOverlay : MonoBehaviour
{
    public Button StayButton, LeaveButton;

    void Start ()
    {
        StayButton.onClick.AddListener(stayClick);
        LeaveButton.onClick.AddListener(leaveClick);
    }

    void stayClick ()
    {
        gameObject.SetActive(false);
    }

    void leaveClick ()
    {
        // TODO: load new scene
        Debug.Log("scene loaded here");
    }
}
