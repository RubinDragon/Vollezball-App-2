using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Doozy.Engine.UI;

public class ViewActive : MonoBehaviour
{
    public static ViewActive instance;

    [Header("References")]
    public TextMeshProUGUI text_successCount;
    public TextMeshProUGUI text_failCount;
    public TextMeshProUGUI text_overallCount;

    public UIView view_finished;

    private float overallCount;
    public float sucessCount;
    public float failCount;

    private void Awake()
    {
        instance = this;
    }

    public void Setup()
    {
        overallCount = AppManager.instance.currTest.repetitions;
    }

    public void LoadAllText()
    {
        text_successCount.SetText(sucessCount.ToString());
        text_failCount.SetText(failCount.ToString());

        text_overallCount.SetText(overallCount.ToString());
    }

    private void CheckForFinished()
    {
        if (overallCount <= 0)
            Finished();
    }

    private void Finished()
    {
        view_finished.Show();

        AppManager.instance.currSuccessCount = sucessCount;
        view_finished.GetComponent<ViewFinished>().LoadData();

        // stats
        bool success = sucessCount >= 16;
        AppManager.instance.IncreaseExercises(success);
        AppManager.instance.IncreaseRepetition();
        AppManager.instance.SaveStats();

        // level system
        if(AppManager.instance.path == AppManager.Path.Test)
        {
            bool testSuccess = sucessCount >= 9;

            if (testSuccess)
            {
                bool stage1completion = sucessCount < 18;
                bool test1to3 = AppManager.instance.level == 1;

                AppManager.instance.TestSuccess(test1to3, stage1completion);
            }
        }

        Invoke(nameof(CustomReset),0.5f);
        GetComponent<UIView>().Hide();
    }

    public void CustomReset()
    {
        overallCount = 0f;
        sucessCount = 0f;
        failCount = 0f;
    }

    #region Button Clicks

    public void OnSuccess()
    {
        overallCount--;
        sucessCount++;

        LoadAllText();
        CheckForFinished();
    }

    public void OnFailed()
    {
        overallCount--;
        failCount++;

        LoadAllText();
        CheckForFinished();
    }

    #endregion
}
