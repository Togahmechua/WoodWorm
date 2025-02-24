using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAbleGameObj : Controller
{
    private List<GameObject> obstacleList = new List<GameObject>();
    private List<PushAbleGameObj> pushAbleList = new List<PushAbleGameObj>();
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        LoadObjList(LevelManager.Ins.level.GameObjList(), LevelManager.Ins.level.PushAbleGameObjList());
    }

    private void Update()
    {
        Fall();
    }

    private void Fall()
    {
        float rayLength = 0.51f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength);

        if (hit.collider != null && (hit.collider.CompareTag("Wall") || hit.collider.CompareTag("Fruit")))
        {
            Vector3 roundedPos = new Vector3(
                Mathf.Round(transform.position.x * 2) / 2,
                Mathf.Round(transform.position.y * 2) / 2,
                transform.position.z
            );

            rb.velocity = Vector2.zero;  // Dừng rơi
            transform.position = roundedPos;  // Đặt về vị trí làm tròn
            return;
        }

        // Nếu không có vật cản, tiếp tục rơi
        rb.velocity = Vector2.down * 3f;
    }


    public override void LoadObjList(List<GameObject> obstacleL, List<PushAbleGameObj> pushAbleL)
    {
        obstacleList.Clear();
        pushAbleList.Clear();

        obstacleList = obstacleL;
        pushAbleList = pushAbleL;
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

    public override bool Blocked(Vector3 postition, Vector2 direction)
    {
        Vector2 newPos = new Vector2(postition.x, postition.y) + direction;

        foreach (var obj in obstacleList)
        {
            if (obj.transform.position.x == newPos.x && obj.transform.position.y == newPos.y)
            {
                return true;
            }
        }

        foreach (PushAbleGameObj objToPush in pushAbleList)
        {
            if (objToPush.transform.position.x == newPos.x && objToPush.transform.position.y == newPos.y)
            {
                return true;
            }
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Vector3.down * 0.51f);
    }
}
