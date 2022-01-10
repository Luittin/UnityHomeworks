using System;
using UnityEngine;

public interface IPoolable
{
    void ReturnToPool();
    void RequestFromPool();
}
