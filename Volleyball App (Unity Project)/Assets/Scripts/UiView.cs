using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiView : MonoBehaviour
{
    public GameObject content;

    private void Start()
    {
        if (gameObject.name == "View - MainScreen")
            transform.Find("Content").gameObject.SetActive(true);
        else
            transform.Find("Content").gameObject.SetActive(false);
    }

    public void Show()
    {

        content.SetActive(true);

    }

    public void Hide()
    {
        content.SetActive(false);
    }
}