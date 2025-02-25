using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private GameObject fruit;

    public static int fruitCount;

    private void ActiveOtherFruit()
    {
        if (fruit != null)
        {
            fruit.SetActive(true);
        }
    }

    public void Eat()
    {
        AudioManager.Ins.PlaySFX(AudioManager.Ins.click);
        fruitCount++;
        ActiveOtherFruit();
        Destroy(gameObject);
    }
}
