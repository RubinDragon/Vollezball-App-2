using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : MonoBehaviour
{
    public static AppManager instance;

    public Path path;
    public enum Path
    {
        Excersise,
        Test,
        Theory,
        Stats
    }

    public Category category;
    public enum Category
    {
        Pass,
        Manchette,
        Service,
        Schlag
    }

    public int level;
    public int excercise;


    public float currSuccessCount;
    public TestData currTest;

    public List<string> positiveMessages;
    public List<string> negativeMessages;

    public PlayerStats stats;

    private void Awake()
    {
        instance = this;
        
    }

    #region Buttons

    public UiView twoLevels;
    public UiView fiveLevels;
    public UiView theory;

    public void OnCategoryButtonClick()

    {

        if (path == Path.Excersise)
            fiveLevels.Show();
        else if (path == Path.Test)
            twoLevels.Show();
        else if (path == Path.Theory)
        {
            theory.Show();
            theoryScript.LoadTestData();

        }
  
    }

    public ViewTest theoryScript;


    #endregion


    #region Paths

    public void SetPath(int index)
    {
        if (index == 0) path = Path.Test;
        if (index == 1) path = Path.Excersise;
        if (index == 2) path = Path.Theory;
        if (index == 3) path = Path.Stats;
    }

    public void SetCategory(int index)
    {
        if (index == 0) category = Category.Pass;
        if (index == 1) category = Category.Manchette; 
        if (index == 2) category = Category.Service; 
        if (index == 3) category = Category.Schlag; 

    }

    public void SetLevel(int index)
    {
        if (index == 0) level = 1;
        if (index == 1) level = 2;
        if (index == 2) level = 3;
        if (index == 3) level = 4;
        if (index == 4) level = 5;

    }

    public void SetExcercise(int index)
    {
        if (index == 0) excercise = 1;
        if (index == 1) excercise = 2;

    }



    #endregion

    #region Getters

    public string GetMessage(bool positive)
    {
        if (positive)
            return positiveMessages[UnityEngine.Random.Range(0, positiveMessages.Count)];
        else
            return negativeMessages[UnityEngine.Random.Range(0, negativeMessages.Count)];
    }

    #endregion
}

[Serializable]
public class PlayerStats
{
    public int totalExcersisesDone;

}
