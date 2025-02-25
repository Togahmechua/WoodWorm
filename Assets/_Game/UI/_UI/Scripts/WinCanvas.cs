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
        AudioManager.Ins.PlaySFX(AudioManager.Ins.win);
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
            AudioManager.Ins.PlaySFX(AudioManager.Ins.click);
            LevelManager.Ins.curMapID++;

            if (LevelManager.Ins.curMapID < LevelManager.Ins.levelList.Count - 1)
            {
                // Load the next level
                LevelManager.Ins.LoadMapByID(LevelManager.Ins.curMapID);
                UIManager.Ins.CloseUI<WinCanvas>();
                UIManager.Ins.OpenUI<MainCanvas>();
            }
            else
            {
                // Reached the last level
                Debug.Log("All levels completed!");
                UIManager.Ins.CloseUI<WinCanvas>();
                UIManager.Ins.CloseUI<MainCanvas>();
                UIManager.Ins.OpenUI<ChooseLevelCanvas>();
            }
        });

        menuBtn.onClick.AddListener(() =>
        {
            AudioManager.Ins.PlaySFX(AudioManager.Ins.click);
            UIManager.Ins.CloseUI<WinCanvas>();
            UIManager.Ins.OpenUI<ChooseLevelCanvas>();
        });

        retryBtn.onClick.AddListener(() =>
        {
            AudioManager.Ins.PlaySFX(AudioManager.Ins.click);
            UIManager.Ins.CloseUI<WinCanvas>();
            UIManager.Ins.OpenUI<MainCanvas>();
            LevelManager.Ins.LoadMapByID(LevelManager.Ins.curMapID);
        });

        soundBtn.onClick.AddListener(() =>
        {
            AudioManager.Ins.PlaySFX(AudioManager.Ins.click);
            isClick = !isClick;
            soundBtn.image.sprite = spr[isClick ? 1 : 0];

            if (isClick)
            {
                AudioManager.Ins.TurnOff();
            }
            else
            {
                AudioManager.Ins.TurnOn();
            }
        });
    }
}
