using System;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField]
    private int damage = 1;

    protected Action<Bullet> onEndLifetime;

    public Action<Bullet> OnEndLifetime { get => onEndLifetime; set => onEndLifetime = value; }
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
