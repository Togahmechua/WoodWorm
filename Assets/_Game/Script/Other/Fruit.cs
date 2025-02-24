using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private int count;

    private static int fruitCount;

    private void Update()
    {
        count = fruitCount;
    }

    public void Eat()
    {
        fruitCount++;
      
        Destroy(gameObject);
    }
}
