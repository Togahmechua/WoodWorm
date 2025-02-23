using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLevelCanvas : UICanvas
{
    [SerializeField] private SpawnLevel spawnLevel;
    [SerializeField] private Button quitBtn;

    private void OnEnable()
    {
        spawnLevel.Check();
    }

    private void Start()
    {
        quitBtn.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
