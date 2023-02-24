using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;


public class PoolMono<T> where T : MonoBehaviour
{
    public bool AutoExpand { get; set; }
    public T Prefab { get; }
    public Transform Container { get; }
        
    protected List<T> pool;
        
    public PoolMono(T prefab, int count)
    {
        Prefab = prefab; 
        Container = null; 
        CreatePool(Prefab, count, Container);
    }

    public PoolMono(T prefab, int count, Transform container)
    {
        Prefab = prefab;
        Container = container; 
        CreatePool(Prefab, count, Container);
    }

    private void CreatePool(T prefab, int count, Transform container)
    {
        pool = new List<T>();

        for (int i = 0; i < count; i++) 
            CreateObject(prefab, container);
    }

    private T CreateObject(T prefab, Transform container, bool isActiveByDefault = false)
    {
        var createdObject = Object.Instantiate(prefab, container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        pool.Add(createdObject);
        return createdObject;
    }

    public bool HasFreeElement(out T element)
    {
        foreach (var mono in pool)
        {
            if (!mono.gameObject.activeInHierarchy)
            {
                mono.gameObject.SetActive(true);
                mono.transform.position = Container.transform.position;
                element = mono;
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeElement()
    {
        if (HasFreeElement(out var element))
            return element;
            
        if (AutoExpand)
            return CreateObject(Prefab, Container, true);

        throw new Exception($"The pool of type {typeof(T).Name} is empty. Current elements number is: {pool.Count}");
    }

    public T[] GetFreeElements(int count)
    {
        var freeElements = new List<T>();
            
        foreach (var mono in pool)
        {
            if (!mono.gameObject.activeInHierarchy)
            {
                freeElements.Add(mono);
                mono.gameObject.SetActive(true);
            }
        }

        if (freeElements.Count < count)
        {
            if (AutoExpand)
            {
                var difference = count - freeElements.Count;
                for (int i = 0; i < difference; i++)
                {
                    var createdObject = CreateObject(Prefab, Container);
                    createdObject.gameObject.SetActive(true);
                    freeElements.Add(createdObject);
                }

                return freeElements.ToArray();
            }    
            
            throw new Exception($"Pool of type {typeof(T).Name} doesn't have so much free elements. Only {freeElements.Count}/{count}");
        }

        return freeElements.ToArray();
    }

    public T[] GetAllElements()
    {
        return pool.ToArray();
    }

    public T[] GetAllActiveElements()
    {
        var activeElements = new List<T>();
        foreach (var element in pool)
        {
            if (element.gameObject.activeInHierarchy)
                activeElements.Add(element);
        }

        return activeElements.ToArray();
    }

    public int GetFreeElementsCount()
    {
        var sum = 0;
        foreach (var mono in pool)
        {
            if (!mono.gameObject.activeInHierarchy)
                sum++;
        }

        return sum;
    }
}
