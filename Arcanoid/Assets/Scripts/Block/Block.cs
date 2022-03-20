using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    private BlockHealth _blockHealth;

    [SerializeField]
    private UnityEngine.GameObject effect;

    public UnityEngine.GameObject Effect { get => effect; set => effect = value; }

    public void DestroyBlock()
    {
        Instantiate(Effect);
        Destroy(this.gameObject);
    }
}
