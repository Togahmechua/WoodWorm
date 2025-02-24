using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAbleGameObj : Controller
{
    private List<GameObject> obstacleList = new List<GameObject>();
    private List<PushAbleGameObj> pushAbleList = new List<PushAbleGameObj>();

    void Start()
    {
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

        if (hit.collider != null)
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                Debug.Log("Raycast chạm chính nó, bỏ qua!");
                return;
            }

            //Debug.Log("Hit: " + hit.collider.gameObject.name);

            if (hit.collider.CompareTag("Wall"))
            {
                //Debug.Log("Wall");
                transform.position = new Vector3(  // 🔹 Làm tròn vị trí để không bị lệch
                Mathf.Round(transform.position.x * 2) / 2, // Làm tròn theo bước 0.5
                Mathf.Round(transform.position.y * 2) / 2,  // Làm tròn theo bước 0.5
                transform.position.z);
                return;
            }
        }
        else
        {
            transform.position += Vector3.down * Time.deltaTime * 3f; // Di chuyển dần xuống
        }
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
        Gizmos.DrawRay(transform.position, Vector3.down * 1f);
    }
}
