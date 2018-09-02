using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIElement : MonoBehaviour {

    [SerializeField]
    private InputField name;
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text timeText;
    [SerializeField]
    private Image indicatorReloadGun;

    [SerializeField]
    private Image handCursor;

    //[SerializeField]
    //private GameObject[] top3 = new GameObject[9];

    [SerializeField]
    private GameObject mvp;

    public void lengthChange(int index)
    {
        Debug.Log(index);
        switch (index)
        {
            case 0:
                RunnerInfo.lengthNum = 0;
                RunnerInfo.lengthWay = 7;
               // Debug.Log("7");
                break;
            case 1:
                RunnerInfo.lengthNum = 3;
                RunnerInfo.lengthWay = 15;
                //Debug.Log("15");
                break;
            case 2:
                RunnerInfo.lengthNum = 6;
                RunnerInfo.lengthWay = 25;
                //Debug.Log("25");
                break;
        }
    }

    public void levelChange(int index)
    {
        switch (index)
        {
            case 0:
                RunnerInfo.levelNum = 0;
                RunnerInfo.easy = true;
                RunnerInfo.density = 0.4f;
                break;
            case 1:
                RunnerInfo.levelNum = 1;
                RunnerInfo.easy = false;
                RunnerInfo.density = 0.2f;
                break;
            case 2:
                RunnerInfo.levelNum = 2;
                RunnerInfo.easy = false;
                RunnerInfo.density = 0.6f;
                break;
        }
    }

    public void nameVision()
    {
        nameText.text = RunnerInfo.name;
    }

    public void timeVision(float time)
    {
        timeText.text = time + "s";
    }

    public void startLevel()
    {
        RunnerInfo.name = name.text;
        Application.LoadLevel("RunnerScene");
    }

    public void endLevel()
    {
        RunnerInfo.easy = true;
        RunnerInfo.density = 0.4f;
        RunnerInfo.lengthWay = 7;
        RunnerInfo.name = "";
        Application.LoadLevel("MainMenu");
    }

    //public void hsInit()
    //{
    //    RunnerInfo.initHS();
    //    for (int i = 0; i < 9; i++)
    //    {
    //        Text[] pos = top3[i].GetComponentsInChildren<Text>();
    //        for (int j = 0; j < RunnerInfo.highscore[i].Count && j < 3; j++)
    //        {
    //            pos[j+1].text = (j + 1) + ": " + RunnerInfo.highscore[i][j].name + " " + RunnerInfo.highscore[i][j].time + "s";
    //        }
    //    }
    //}

    public void hsInit()
    {
        RunnerInfo.initHS();
           Text pos = mvp.GetComponent<Text>();
        if(RunnerInfo.highscore.Count > 0)
           pos.text = RunnerInfo.highscore[0].name + " " + RunnerInfo.highscore[0].time + "s";
    }

    public void reloadGun(float pr)
    {
        float width = 300;
        indicatorReloadGun.rectTransform.sizeDelta = new Vector2(width*pr, indicatorReloadGun.rectTransform.rect.height);
    }

    public void reactivateRunnerCursor(bool flag, int index)
    {
        if (flag != handCursor.gameObject.activeSelf)
            handCursor.gameObject.SetActive(flag);

    }

    public void setPosRunnerCursor(Vector2 pos, int index)
    {
        pos.y = handCursor.rectTransform.anchoredPosition.y;
        handCursor.rectTransform.anchoredPosition = pos;
        
    }

    public void setRotRunnerCursor(float rot, int index)
    {
        
        handCursor.rectTransform.rotation = Quaternion.Euler(new Vector3(0, 0, rot));
        
    }

    public void deactivateStartMenu()
    {
        handCursor.transform.parent.gameObject.SetActive(false);
    }

    public void home()
    {
        Application.LoadLevel("MainMenu");
    }
}
