using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockButton : MonoBehaviour
{
    public int minLevelRequired;

    private void Start()
    {
        Refresh();

        // subscribe to events
        AppManager.instance.testDone += Refresh;
        print("subscribed");
    }

    private void Refresh()
    {
        if(AppManager.instance.GetLevel(AppManager.instance.category) < minLevelRequired)
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
