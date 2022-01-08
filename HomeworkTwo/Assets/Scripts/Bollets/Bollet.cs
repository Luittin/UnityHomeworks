using System;
using UnityEngine;

public class Bollet : MonoBehaviour, IPoolable
{
    [SerializeField]
    private int damage = 1;

    protected Action<Bollet> onEndLifetime;

    public Action<Bollet> OnEndLifetime { get => onEndLifetime; set => onEndLifetime = value; }
    public int Damage { get => damage; set => damage = value; }

    public virtual void RequestFromPool()
    {
        gameObject.SetActive(true);
    }

    public virtual void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}
