using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public MapSO mapSO;
    [HideInInspector] public Level level;
    public List<Level> levelList = new List<Level>();
    public int curMap;
    public bool isWin;
    public int curMapID;

    private List<Level> curLevelList = new List<Level>();

    private void Start()
    {
        curMap = PlayerPrefs.GetInt("CurrentMap", 0);
        mapSO.LoadWinStates();
    }

    public void LoadMapByID(int id)
    {
        curMapID = id;

        if (level != null)
        {
            DespawnMap();
        }

        foreach (Level lv in levelList)
        {
            if (lv.id == id)
            {
                level = Instantiate(levelList[id], transform);
                curLevelList.Add(level);
            }
        }
    }

    public void DespawnMap()
    {
        if (level != null)
        {
            foreach (Level lv in curLevelList)
            {
                Destroy(level.gameObject); ;
            }
            
            curLevelList.Clear();
            level = null;
            isWin = false;
        }
    }
}
