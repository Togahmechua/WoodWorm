using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache 
{
    private static Dictionary<Collider2D, Fruit> fruits = new Dictionary<Collider2D, Fruit>();

    public static Fruit GetFruit(Collider2D collider)
    {
        if (!fruits.ContainsKey(collider))
        {
            fruits.Add(collider, collider.GetComponent<Fruit>());
        }

        return fruits[collider];
    }
}
