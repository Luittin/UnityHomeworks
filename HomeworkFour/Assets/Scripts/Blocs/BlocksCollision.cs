using System;
using UnityEngine;

public class BlocksCollision : MonoBehaviour
{
    private Action onCollisionPlayer;

    private void Awake()
    {
        onCollisionPlayer += GameManager.Instance().StopGame;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            onCollisionPlayer?.Invoke();
        }
    }
}
