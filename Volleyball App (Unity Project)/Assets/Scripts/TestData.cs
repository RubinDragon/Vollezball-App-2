using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[Serializable]
[CreateAssetMenu(fileName = "NewTestData", menuName = "Custom/Test")]
public class TestData : ScriptableObject
{
    public string title;
    public string description;

    public int repetitions;
    
    public int level;
    public int excercise;

    public AppManager.Category category;
    public AppManager.Path path;

    // video
    public VideoClip videoClip;
    /// public Sprite sprite;
   
}


