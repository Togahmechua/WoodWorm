using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controller : MonoBehaviour
{
    public abstract void LoadObjList(List<GameObject> list, List<PushAbleGameObj> push);

    public abstract bool Move(Vector2 direction);

    public abstract bool Blocked(Vector3 postition, Vector2 direction);
}
