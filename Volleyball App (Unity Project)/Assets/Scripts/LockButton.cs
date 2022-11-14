using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockButton : MonoBehaviour
{
    public int minLevelRequired;
    



    private void Update()
    {
        if (AppManager.instance.GetLevel(AppManager.instance.category) < minLevelRequired)
        {
            GetComponent<Button>().interactable = false;
        }
        else
        {
            GetComponent<Button>().interactable = true;
        }

    }

    //private void Awake()
    //{

    //  if (AppManager.instance.GetLevel(AppManager.instance.category) < minLevelRequired)
    //  {
    //          GetComponent<Button>().interactable = false;
    //  }
    //   else
    //   {
    //    GetComponent<Button>().interactable = true;
    //   }

    //}


}
