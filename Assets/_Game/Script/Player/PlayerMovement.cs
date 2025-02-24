using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Controller
{
    [SerializeField] private List<GameObject> obstacleList = new List<GameObject>();
    [SerializeField] private List<PushAbleGameObj> pushAbleList = new List<PushAbleGameObj>();
    [SerializeField] private Transform model;
    [SerializeField] private List<Transform> snakeBody = new List<Transform>();

    private bool isReadyToMove;
    private List<Vector3> previousPositions = new List<Vector3>(); // 🔹 Lưu vị trí cũ để phần thân follow
    private List<Vector3> prePosOfHead = new List<Vector3>();
    private Vector2 lastDirection = Vector2.right;
    private Transform head;
    private int currentBodyIndex = 1;
    private bool blockRight, blockLeft, blockUp;
    void Start()
    {
        head = model.GetChild(0);
        LoadBody();
        LoadObjList(LevelManager.Ins.level.GameObjList(), LevelManager.Ins.level.PushAbleGameObjList());
    }

    void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput.Normalize();

        if (moveInput.sqrMagnitude > 0.5)
        {
            if (isReadyToMove && !IsReverseMove(moveInput))
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

    private void Check()
    {
        float rayLength = 0.51f;

        RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.right, rayLength);
        RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector2.left, rayLength);
        RaycastHit2D upHit = Physics2D.Raycast(transform.position, Vector2.up, rayLength);

        blockRight = rightHit.collider != null && rightHit.collider.CompareTag("Wall");
        blockLeft = leftHit.collider != null && leftHit.collider.CompareTag("Wall");
        blockUp = upHit.collider != null && upHit.collider.CompareTag("Wall");

        Debug.Log($"Block Right: {blockRight}, Block Left: {blockLeft}, Block Up: {blockUp}");
    }

    public override void LoadObjList(List<GameObject> obstacleL, List<PushAbleGameObj> pushAbleL)
    {
        obstacleList.Clear();
        pushAbleList.Clear();

        obstacleList = obstacleL;
        pushAbleList = pushAbleL;
    }

    private bool IsReverseMove(Vector2 direction)
    {
        // Nếu số lượng thân > 0, kiểm tra có đi ngược lại hay không
        if (snakeBody.Count > 0)
        {
            Vector2 reverseDirection = -lastDirection; // Hướng ngược lại
            if (direction == reverseDirection)
            {
                Debug.Log("Không thể đi ngược lại!");
                return true;
            }
        }
        return false;
    }

    public override bool Move(Vector2 direction)
    {
        Check(); // Gọi Check trước khi di chuyển

        if ((direction.x > 0 && blockRight) || (direction.x < 0 && blockLeft) || (direction.y > 0 && blockUp))
        {
            Debug.Log("Không thể di chuyển về hướng này!");
            return false;
        }

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y)) // Ưu tiên di chuyển ngang
        {
            direction.y = 0;

            if (direction.x > 0) // Đi sang phải
            {
                head.localScale = new Vector3(1, 1, 1);

                for (int i = 0; i < snakeBody.Count; i++)
                {
                    snakeBody[i].localScale = new Vector3(1, 1, 1);
                }
            }
            else if (direction.x < 0) // Đi sang trái
            {
                head.localScale = new Vector3(-1, 1, 1);

                for (int i = 0; i < snakeBody.Count; i++)
                {
                    snakeBody[i].localScale = new Vector3(-1, 1, 1);
                }
            }
        }
        else // Ưu tiên di chuyển dọc
        {
            direction.x = 0;
        }

        direction.Normalize();

        if (Blocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            // 🔹 Lưu vị trí hiện tại trước khi di chuyển
            previousPositions.Insert(0, transform.position);

            // 🔹 Giới hạn danh sách để không lưu quá nhiều vị trí
            if (previousPositions.Count > snakeBody.Count + 1)
            {
                previousPositions.RemoveAt(previousPositions.Count - 1);
            }

            lastDirection = direction;

            // 🔹 Di chuyển đầu rắn
            transform.Translate(direction);

            // 🔹 Di chuyển từng phần thân đến vị trí trước đó
            for (int i = 0; i < snakeBody.Count; i++)
            {
                if (i < previousPositions.Count)
                {
                    snakeBody[i].transform.position = previousPositions[i];
                }
            }

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

    private void LoadBody()
    {
        snakeBody.Clear();

        for (int i = 1; i < model.childCount; i++)
        {
            Transform body = model.GetChild(i).transform;
            if (body != null)
            {
                snakeBody.Add(body);
            }
        }
    }

    private void InstantiateBody()
    {
        if (currentBodyIndex < snakeBody.Count)
        {
            snakeBody[currentBodyIndex].gameObject.SetActive(true);
            currentBodyIndex++;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Fruit fruit = Cache.GetFruit(other);
        if (fruit != null)
        {
            fruit.Eat();
            InstantiateBody();
        }
    }
}
