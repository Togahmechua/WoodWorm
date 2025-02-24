using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseCanvas : UICanvas
{
    [Header("---Other Button---")]
    [SerializeField] private Button continueBtn;
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
        continueBtn.onClick.AddListener(() =>
        {
            UIManager.Ins.CloseUI<PauseCanvas>();
            UIManager.Ins.OpenUI<MainCanvas>();
        });

        menuBtn.onClick.AddListener(() =>
        {
            UIManager.Ins.CloseUI<PauseCanvas>();
            UIManager.Ins.OpenUI<ChooseLevelCanvas>();
        });

        retryBtn.onClick.AddListener(() =>
        {
            UIManager.Ins.CloseUI<PauseCanvas>();
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
