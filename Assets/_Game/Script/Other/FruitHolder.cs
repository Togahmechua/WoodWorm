using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitHolder : MonoBehaviour
{
    private bool flag;

    private void Update()
    {
        if (transform.childCount == 0 && !flag)
        {
            DestroyGate();
            flag = true;
        }
    }

    public void DestroyGate()
    {
        DestroyImmediate(this.gameObject);
        LevelManager.Ins.level.LoadList();
    }
}
