using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ViewActive : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI text_successCount;
    public TextMeshProUGUI text_failCount;
    public TextMeshProUGUI text_overallCount;

    public UiView view_finished;

    private float overallCount;
    private float sucessCount;
    private float failCount;

    public void LoadAllText()
    {
        text_successCount.SetText(sucessCount.ToString());
        text_failCount.SetText(failCount.ToString());

        text_overallCount.SetText(overallCount.ToString() + "/" + AppManager.instance.currTest.repetitions);
    }

    private void CheckForFinished()
    {
        if (overallCount >= AppManager.instance.currTest.repetitions)
            Finished();
    }

    private void Finished()
    {
        view_finished.Show();

        AppManager.instance.currSuccessCount = sucessCount;
        view_finished.GetComponent<ViewFinished>().LoadData();

        // stats
        bool success = sucessCount > failCount;
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
        GetComponent<UiView>().Hide();

        AppManager.instance.StopDaDound();
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
        overallCount++;
        sucessCount++;

        LoadAllText();
        CheckForFinished();
    }

    public void OnFailed()
    {
        overallCount++;
        failCount++;

        LoadAllText();
        CheckForFinished();
    }

    #endregion
}
