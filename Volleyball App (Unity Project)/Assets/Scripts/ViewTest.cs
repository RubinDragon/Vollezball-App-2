using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ViewTest : MonoBehaviour
{
    public List<TestData> testDatas;

    public TextMeshProUGUI text_title;
    public TextMeshProUGUI text_description;

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

        FindObjectOfType<ViewActive>().LoadAllText();


    }
}
