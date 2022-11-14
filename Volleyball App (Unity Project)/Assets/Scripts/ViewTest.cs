using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;

public class ViewTest : MonoBehaviour
{
    public List<TestData> testDatas;

    public TextMeshProUGUI text_title;
    public TextMeshProUGUI text_description;

    public RectTransform scrollRect;

    public VideoPlayer videoPlayer;

    public Image image;
    public void LoadTestData()
    {
        TestData data = null;

        for (int i = 0; i < testDatas.Count; i++)
        {
            TestData t = testDatas[i];

            if (t.path == AppManager.instance.path && t.category == AppManager.instance.category && t.level == AppManager.instance.level && t.excercise == AppManager.instance.excercise)
                data = t;

        }

        print(AppManager.instance.excercise);
        text_title.SetText(data.title);
        text_description.SetText(data.description);

        AppManager.instance.currTest = data;

        LayoutRebuilder.ForceRebuildLayoutImmediate(scrollRect);

        ViewActive.instance.LoadAllText();

        // video
        if (data.videoClip != null)
        {
            videoPlayer.gameObject.SetActive(true);
            videoPlayer.clip = data.videoClip;
            videoPlayer.Play();
        }
        else
            if(videoPlayer != null) videoPlayer.gameObject.SetActive(false);

        LayoutRebuilder.ForceRebuildLayoutImmediate(scrollRect);

        // Bild
        ///image.sprite = data.sprite;
    }
}
