using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeFruit
{
    apple, 
    orrange,
    banana
}

public class Fruit : MonoBehaviour
{
    [Header("Set In Inspector")]
    [SerializeField] private TypeFruit type;
    [SerializeField] private int _points;

    public static event Action<int> FruitClicked;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Player player))
        {
            FruitClicked?.Invoke(_points);
            Destroy(this.gameObject);
        }
    }
}
