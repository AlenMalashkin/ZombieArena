using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class EnemyPool : MonoBehaviour
{
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private int _enemyCountInPool;
    [SerializeField] private float _timeToGetEnemyFromPool;
    [SerializeField] private bool _autoExpand;
    
    private DiContainer _diContainer;
    private List<Enemy> _enemyPool;
    private Timer _timer;

    [Inject]
    private void Construct(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }
    
    private void OnEnable()
    {
        _timer = new Timer(TimerType.UpdateTick);
        _timer.TimerFinished += GetEnemyFromPool;

        CreatePool(_enemyCountInPool);

        GetEnemyFromPool();
    }

    private void OnDisable()
    {
        _timer.TimerFinished -= GetEnemyFromPool;
    }

    private void CreatePool(int count)
    {
        _enemyPool = new List<Enemy>();

        for (int i = 0; i < count; i++) 
            Create(_enemyPrefab, transform);
    }
    
    private Enemy Create(Enemy prefab, Transform container, bool isActiveByDefault = false)
    {
        var createdObject = _diContainer.InstantiatePrefabForComponent<Enemy>
        (
            prefab,
            _spawnPoints[Random.Range(0, _spawnPoints.Length)].position,
            Quaternion.identity,
            container
        );

        createdObject.gameObject.SetActive(isActiveByDefault);
        _enemyPool.Add(createdObject);
        return createdObject;
    }

    private bool HasFreeElement(out Enemy element)
    {
        foreach (var enemy in _enemyPool)
        {
            if (!enemy.gameObject.activeInHierarchy)
            {
                enemy.gameObject.SetActive(true);
                element = enemy;
                return true;
            }
        }

        element = null;
        return false;
    }

    private void GetEnemyFromPool()
    {
        _timer.Start(_timeToGetEnemyFromPool);
        
        if (HasFreeElement(out var element))
            return;
            
        if (_autoExpand)
            Create(_enemyPrefab, transform, true);
        
        
        throw new Exception($"The pool of type {typeof(Enemy).Name} is empty. Current elements number is: {_enemyPool.Count}");
    }

    public Enemy[] GetActiveEnemies()
    {
        var activeElements = new List<Enemy>();
        foreach (var element in _enemyPool)
        {
            if (element.gameObject.activeInHierarchy)
                activeElements.Add(element);
        }

        return activeElements.ToArray();
    }
}
