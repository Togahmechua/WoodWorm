using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBrick : MonoBehaviour
{
    [SerializeField] private GameObject brick;

    private bool flag;
    private bool isTouch;

    private void Update()
    {
        if (!flag && brick != null)
        {
            if (!isTouch)
                return;

            DestroyImmediate(brick);
            Destroy(this.gameObject);
            LevelManager.Ins.level.LoadList();
            flag = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerMovement player = Cache.GetPlayerMovement(other);
        if (player != null)
        {
            isTouch = true;
        }
    }
}
