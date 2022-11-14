using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ViewFinished : MonoBehaviour
{
    public TextMeshProUGUI text_percentage;
    public TextMeshProUGUI text_rating;
    public TextMeshProUGUI text_message;

    public ViewActive viewActive;

    public TextMeshProUGUI text_successCount;
    public TextMeshProUGUI text_failCount;

    private void Start()
    {
        
    }

    public void LoadData()
    {
        text_successCount.SetText(viewActive.sucessCount.ToString());
        text_failCount.SetText(viewActive.failCount.ToString());

        float percentage = (AppManager.instance.currSuccessCount / AppManager.instance.currTest.repetitions) * 100;
        percentage = Mathf.RoundToInt(percentage);

        text_percentage.SetText(percentage.ToString() + "%");

        string rating = percentage >= 80 ? "Geschafft" : "Nicht Geschafft";
        if (percentage == 69) rating = "Nice :)";
        text_rating.SetText(rating);

        bool positive = percentage >= 80;
        text_message.SetText(AppManager.instance.GetMessage(positive));
    }
}
