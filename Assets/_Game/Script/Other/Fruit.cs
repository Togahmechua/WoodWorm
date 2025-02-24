using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public static int fruitCount;

    public void Eat()
    {
        fruitCount++;
      
        Destroy(gameObject);
    }
}
