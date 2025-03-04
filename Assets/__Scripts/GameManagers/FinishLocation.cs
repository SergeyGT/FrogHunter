using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLocation : MonoBehaviour
{
    public static event Action Finish;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Player player))
        {
            Finish?.Invoke();
        }
    }
}
