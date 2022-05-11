using System;
using System.Collections.Generic;

public class ObjectPool<T> where T : IPoolable
{
    private Queue<T> _freeBullet;
    private Func<T> _createObject;
    public ObjectPool(Func<T> createObject)
    {
        _freeBullet = new Queue<T>();
        _createObject = createObject;
    }

    public T GetBullet()
    {
        T objectToReturn = _freeBullet.Count > 0 ? _freeBullet.Dequeue() : CreateNewObject();
        objectToReturn.RequestFromPool();
        return objectToReturn;
    }

    public void ReturnBullet(T bullet)
    {
        bullet.ReturnToPool();
        _freeBullet.Enqueue(bullet);
    }

    public T CreateNewObject()
    {
        return _createObject.Invoke();
    }
}
