using System;
using System.Collections.Generic;

public class ObjectPool<T> where T : IPoolable
{
    private Queue<T> _freeObject;
    private Func<T> _createObject;
    public ObjectPool(Func<T> createObject)
    {
        _freeObject = new Queue<T>();
        _createObject = createObject;
    }

    public T GetObject()
    {
        T objectToReturn = _freeObject.Count > 0 ? _freeObject.Dequeue() : CreateNewObject();
        objectToReturn.RequestFromPool();
        return objectToReturn;
    }

    public void ReturnObject(T anObject)
    {
        anObject.ReturnToPool();
        _freeObject.Enqueue(anObject);
    }

    public T CreateNewObject()
    {
        return _createObject.Invoke();
    }
}
