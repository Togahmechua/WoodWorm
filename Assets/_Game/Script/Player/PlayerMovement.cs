using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Controller
{
    [SerializeField] private List<GameObject> obstacleList = new List<GameObject>();
    [SerializeField] private List<PushAbleGameObj> pushAbleList = new List<PushAbleGameObj>();

    private bool isReadyToMove;

    void Start()
    {
        LoadObjList(LevelManager.Ins.level.GameObjList(), LevelManager.Ins.level.PushAbleGameObjList());
    }

    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput.Normalize();

        if (moveInput.sqrMagnitude > 0.5)
        {
            if (isReadyToMove)
            {
                isReadyToMove = false;
                Move(moveInput);
            }
        }
        else
        {
            isReadyToMove =  true;
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
        if (Mathf.Abs(direction.x) < 0.5)
        {
            direction.x = 0;
        }
        else
        {
            direction.y = 0;
        }

        direction.Normalize();

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
                if (objToPush != null && objToPush.Move(direction))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        return false;
    }
}
