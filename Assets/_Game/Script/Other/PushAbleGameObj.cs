using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAbleGameObj : Controller
{
    private List<GameObject> obstacleList = new List<GameObject>();
    private List<PushAbleGameObj> pushAbleList = new List<PushAbleGameObj>();
    private bool isFalling = false;
    private float fallDelay = 0.15f; // Thời gian giữa mỗi lần rơi

    void Start()
    {
        LoadObjList(LevelManager.Ins.level.GameObjList(), LevelManager.Ins.level.PushAbleGameObjList());
        StartCoroutine(FallLoop());
    }

    private IEnumerator FallLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(fallDelay);

            // Nếu bên dưới là "Fruit" thì không rơi nữa
            if (CheckBelowIsFruit())
            {
                isFalling = false;
                AlignToGrid();
                continue;
            }

            if (!Blocked(transform.position, Vector2.down))
            {
                Move(Vector2.down);
                isFalling = true;
            }
            else
            {
                if (isFalling)
                {
                    isFalling = false;
                    AlignToGrid();
                }
            }
        }
    }

    private bool CheckBelowIsFruit()
    {
        float rayLength = 0.6f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength);

        if (hit.collider != null && hit.collider.CompareTag("Fruit"))
        {
            return true;
        }
        return false;
    }

    private void AlignToGrid()
    {
        transform.position = new Vector3(
            Mathf.Round(transform.position.x * 2) / 2,
            Mathf.Round(transform.position.y * 2) / 2,
            transform.position.z
        );
    }

    public override void LoadObjList(List<GameObject> obstacleL, List<PushAbleGameObj> pushAbleL)
    {
        obstacleList.Clear();
        pushAbleList.Clear();

        obstacleList = new List<GameObject>(obstacleL);
        pushAbleList = new List<PushAbleGameObj>(pushAbleL);
    }

    public override bool Move(Vector2 direction)
    {
        if (Blocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            transform.Translate(direction);
            return true;
        }
    }

    public override bool Blocked(Vector3 position, Vector2 direction)
    {
        Vector2 newPos = new Vector2(position.x, position.y) + direction;

        // Kiểm tra danh sách obstacleList
        for (int i = obstacleList.Count - 1; i >= 0; i--)
        {
            if (obstacleList[i] == null)
            {
                obstacleList.RemoveAt(i);
                continue;
            }

            if (obstacleList[i].transform.position.x == newPos.x && obstacleList[i].transform.position.y == newPos.y)
            {
                return true;
            }
        }

        // Kiểm tra danh sách pushAbleList
        for (int i = pushAbleList.Count - 1; i >= 0; i--)
        {
            if (pushAbleList[i] == null)
            {
                pushAbleList.RemoveAt(i);
                continue;
            }

            if (pushAbleList[i].transform.position.x == newPos.x && pushAbleList[i].transform.position.y == newPos.y)
            {
                return true;
            }
        }

        return false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * 0.6f);
    }
}
