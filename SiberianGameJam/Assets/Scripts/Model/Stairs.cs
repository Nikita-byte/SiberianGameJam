using System;
using UnityEngine;


public class Stairs : MonoBehaviour
{
    public Action OnStair = delegate { };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.gameObject.GetComponent<Player>();

        if (player != null)
        {
            OnStair.Invoke();
        }
    }
}
