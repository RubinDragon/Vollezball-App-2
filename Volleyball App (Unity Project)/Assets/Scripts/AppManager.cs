using TMPro;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Doozy.Engine.UI;

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

    public event Action testDone;

    private void Awake()
    {
        instance = this;

        LoadStats();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            stats = new PlayerStats();
            SaveStats();
        }

        RefreshButtons();
        DisplayStats();

    }

    public void RefreshButtons()
    {
        if(testDone != null) testDone();
    }

    public AudioSource source;
    public void PlayDaSound()
    {
        source.Play();
    }
    public void StopDaDound()
    {
        source.Stop();
    }

    #region Buttons

    public UIView twoLevels;
    public UIView fiveLevels;
    public UIView theory; 

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

    #region Stats

    // Stats Screen:
    // - total exercises done
    // - total repetitions
    // - average percent (maybe failed, suceeded)

    public PlayerStats stats;

    public void LoadStats()
    {
        stats = new PlayerStats();

        if(PlayerPrefs.HasKey("TotalExercises"))
            stats.totalExercises = PlayerPrefs.GetInt("TotalExercises");

        if (PlayerPrefs.HasKey("TotalRepetitions"))
            stats.totalRepetitions = PlayerPrefs.GetInt("TotalRepetitions");

        if (PlayerPrefs.HasKey("AveragePercent"))
            stats.averagePercent = PlayerPrefs.GetFloat("AveragePercent");

        // level

        if (PlayerPrefs.HasKey("PassLevel"))
            stats.passLevel = PlayerPrefs.GetInt("PassLevel");

        if (PlayerPrefs.HasKey("ServiceLevel"))
            stats.serviceLevel = PlayerPrefs.GetInt("ServiceLevel");

        if (PlayerPrefs.HasKey("ManchetteLevel"))
            stats.manchetteLevel = PlayerPrefs.GetInt("ManchetteLevel");

        if (PlayerPrefs.HasKey("SchlagLevel"))
            stats.schlagLevel = PlayerPrefs.GetInt("SchlagLevel");
    }

    public void SaveStats()
    {
        PlayerPrefs.SetInt("TotalExercises", stats.totalExercises);
        PlayerPrefs.SetInt("TotalRepetitions", stats.totalRepetitions);
        PlayerPrefs.SetFloat("AveragePercent", stats.averagePercent);

        // level

        PlayerPrefs.SetInt("PassLevel", stats.passLevel);
        PlayerPrefs.SetInt("ServiceLevel", stats.serviceLevel);
        PlayerPrefs.SetInt("ManchetteLevel", stats.manchetteLevel);
        PlayerPrefs.SetInt("SchlagLevel", stats.schlagLevel);
    }

    public void IncreaseRepetition() { stats.totalRepetitions++; }
    public void IncreaseExercises(bool success) 
    { 
        stats.totalExercises++;

        // success
        if(success)
        {
            stats.exercisesSucceeded++;
        }

        // recalculate average
        stats.averagePercent = ((float)stats.exercisesSucceeded / (float)stats.totalExercises) * 100f;
        stats.averagePercent = Round(stats.averagePercent, 1);
    }

    // stage 1: 45%+
    // stage 2: 85%+
    public void TestSuccess(bool test1to3, bool stage1completion)
    {
        if (test1to3 && stage1completion) SetLevel(category, 2);
        if (test1to3 && !stage1completion) SetLevel(category, 3);
        if (!test1to3 && stage1completion) SetLevel(category, 4);
        if (!test1to3 && !stage1completion) SetLevel(category, 5);

        if (testDone != null)
        {
            testDone();
            print("event called");
        }
    }

    public TextMeshProUGUI totalExercises;
    public TextMeshProUGUI totalReps;
    public TextMeshProUGUI totalSucc;
    public TextMeshProUGUI averageSucc;
    public TextMeshProUGUI levelPass;
    public TextMeshProUGUI levelManchette;
    public TextMeshProUGUI levelSlap;
    public TextMeshProUGUI levelServe;

    public void DisplayStats()
    {
        totalExercises.SetText("Totale Übungen: " + stats.totalExercises);
        totalReps.SetText("Totale Wiederholungen: " + stats.totalRepetitions);
        totalSucc.SetText("Totale Erfolgreiche Übungen: " + stats.exercisesSucceeded);
        averageSucc.SetText("Durchschnittliche Erfolgsrate: " + stats.averagePercent);
        levelPass.SetText("Level Pass: " + stats.passLevel);
        levelManchette.SetText("Level Manchette: " + stats.manchetteLevel);
        levelSlap.SetText("Level Schlag: " + stats.serviceLevel);
        levelServe.SetText("Level Service: " + stats.schlagLevel);
    }



    public void SetLevel(Category category, int level)
    {
        if (category == Category.Pass)
            stats.passLevel = level;

        if (category == Category.Service)
            stats.serviceLevel = level;

        if (category == Category.Manchette)
            stats.manchetteLevel = level;

        if (category == Category.Schlag)
            stats.schlagLevel = level;

        SaveStats();
    }

    public int GetLevel(Category category)
    {
        if (category == Category.Pass)
            return stats.passLevel;

        if (category == Category.Service)
            return stats.serviceLevel;

        if (category == Category.Manchette)
            return stats.manchetteLevel;

        if (category == Category.Schlag)
            return stats.schlagLevel;

        return 0;
    }

    public static float Round(float value, int digits)
    {
        float mult = Mathf.Pow(10.0f, (float)digits);
        return Mathf.Round(value * mult) / mult;
    }

    #endregion
}

[Serializable]
public class PlayerStats
{
    [Header("Stats")]
    public int totalExercises;
    public int exercisesSucceeded;
    public int totalRepetitions;
    public float averagePercent;

    [Header("Level")]
    public int passLevel = 1;
    public int manchetteLevel = 1;
    public int serviceLevel = 1;
    public int schlagLevel = 1;
}
