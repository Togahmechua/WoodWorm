using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int id;
    public ELevel eLevl;

    [SerializeField] private Transform obstacleHolder;
    [SerializeField] private Transform pushAbleHolder;

    [SerializeField] private List<GameObject> obstacleList = new List<GameObject>();
    [SerializeField] private List<PushAbleGameObj> pushAbleList = new List<PushAbleGameObj>();

    private void Update()
    {
        if (!LevelManager.Ins.isWin)
            return;

        if (eLevl == LevelManager.Ins.mapSO.mapList[LevelManager.Ins.curMap].eLevel &&
               !LevelManager.Ins.mapSO.mapList[LevelManager.Ins.curMap].isWon)
        {
            LevelManager.Ins.mapSO.mapList[LevelManager.Ins.curMap].isWon = true;
            SaveWinState(LevelManager.Ins.curMap);
            Debug.Log("Map " + LevelManager.Ins.curMap + " is won.");
            LevelManager.Ins.curMap++;
        }

        SetCurMap();
    }

    public List<GameObject> GameObjList()
    {
        return obstacleList;
    }

    public List<PushAbleGameObj> PushAbleGameObjList()
    {
        return pushAbleList;
    }

    private void SetCurMap()
    {
        PlayerPrefs.SetInt("CurrentMap", LevelManager.Ins.curMap);
        PlayerPrefs.Save();
    }

    private void SaveWinState(int mapIndex)
    {
        string key = "MapWin_" + mapIndex;
        PlayerPrefs.SetInt(key, 1); // Lưu lại trạng thái thắng của map
        PlayerPrefs.Save();
        LevelManager.Ins.mapSO.LoadWinStates();
    }

    private void OnDrawGizmosSelected()
    {
        obstacleList.Clear();
        pushAbleList.Clear();

        for (int i = 0; i < obstacleHolder.childCount; i++)
        {
            GameObject obstacle = obstacleHolder.GetChild(i).gameObject;
            if (obstacle != null)
            {
                obstacleList.Add(obstacle);
            }
        }

        for (int i = 0; i < pushAbleHolder.childCount; i++)
        {
            PushAbleGameObj push = pushAbleHolder.GetChild(i).GetComponent<PushAbleGameObj>();
            if (push != null)
            {
                pushAbleList.Add(push);
            }    
        }
    }
}
