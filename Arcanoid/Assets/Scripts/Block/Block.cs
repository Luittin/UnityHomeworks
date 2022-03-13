using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    private Action _destroyBloc;

    [SerializeField]
    private BlockHealth _blockHealth;

    public Action DestroyBloc { get => _destroyBloc; set => _destroyBloc = value; }

    public void DestroyBlock()
    {
        Destroy(this.gameObject);
    }
}
