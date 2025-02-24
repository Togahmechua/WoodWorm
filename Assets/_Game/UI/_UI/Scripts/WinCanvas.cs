using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCanvas : UICanvas
{
    [Header("---Other Button---")]
    [SerializeField] private Button nextBtn;
    [SerializeField] private Button menuBtn;
    [SerializeField] private Button retryBtn;

    [Header("---Music Button---")]
    [SerializeField] private Button soundBtn;
    [SerializeField] private Sprite[] spr;

    private bool isClick;

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }


    private void Start()
    {
        nextBtn.onClick.AddListener(() =>
        {
            UIManager.Ins.CloseUI<WinCanvas>();
            UIManager.Ins.OpenUI<MainCanvas>();

            int newCurMapID = LevelManager.Ins.curMapID + 1;
            LevelManager.Ins.LoadMapByID(newCurMapID);
        });

        menuBtn.onClick.AddListener(() =>
        {
            UIManager.Ins.CloseUI<WinCanvas>();
            UIManager.Ins.OpenUI<ChooseLevelCanvas>();
        });

        retryBtn.onClick.AddListener(() =>
        {
            UIManager.Ins.CloseUI<WinCanvas>();
            UIManager.Ins.OpenUI<MainCanvas>();
            LevelManager.Ins.LoadMapByID(LevelManager.Ins.curMapID);
        });

        soundBtn.onClick.AddListener(() =>
        {
            isClick = !isClick;
            soundBtn.image.sprite = spr[isClick ? 1 : 0];

            /*if (isClick)
            {
                AudioManager.Ins.TurnOff();
            }
            else
            {
                AudioManager.Ins.TurnOn();
            }*/
        });
    }
}
