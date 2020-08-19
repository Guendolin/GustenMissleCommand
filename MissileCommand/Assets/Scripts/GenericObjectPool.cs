using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericObjectPool<T> : MonoBehaviour where T : Component
{
    [SerializeField]
    private T prefab;

    public static GenericObjectPool<T> Instance { get; private set; }
    private Queue<T> objects = new Queue<T>();
    public List<T> ObjectList;


    private void Awake()
    {
        Instance = this;
    }

    public T Get()
    {
        if (objects.Count == 0)
            AddObjects(1);

        var dequeueObject = objects.Dequeue();
        
        ObjectList.Add(dequeueObject);
        return dequeueObject;
    }

    public void ReturnToPool(T objectsToReturn)
    {
        ObjectList.Remove(objectsToReturn);
        
        objectsToReturn.gameObject.SetActive(false);
        objects.Enqueue(objectsToReturn);
    }

    private void AddObjects(int count)
    {
        var newObject = GameObject.Instantiate(prefab);
        newObject.gameObject.SetActive(false);
        objects.Enqueue(newObject);
    }
}
