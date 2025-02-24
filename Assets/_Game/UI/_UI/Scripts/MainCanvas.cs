using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : UICanvas
{
    [SerializeField] private Button pauseBtn;
    [SerializeField] private Button retryBtn;
    [SerializeField] private Text levelTxt;

    private void OnEnable()
    {
        levelTxt.text = "LEVEL " + LoadText(); 
    }

    private void Start()
    {
        pauseBtn.onClick.AddListener(() =>
        {
            UIManager.Ins.OpenUI<PauseCanvas>();
            UIManager.Ins.CloseUI<MainCanvas>();
        });

        retryBtn.onClick.AddListener(() =>
        {
            LevelManager.Ins.LoadMapByID(LevelManager.Ins.curMapID);
        });
    }

    private string LoadText()
    {
        string txt = "";
        int num = LevelManager.Ins.curMapID + 1;

        if (num <= 9)
        {
            txt = "0" + num.ToString();
        }
        else
        {
            txt = num.ToString();
        }

        return txt;
    }
}
