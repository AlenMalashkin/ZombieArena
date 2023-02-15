using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] private WeaponAbstract _weapon;
    [SerializeField] private Button _button;

    private Storage _storage;
    private GameData _gameData;

    private void OnEnable()
    {
        _button.onClick.AddListener(BuyItem);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(BuyItem);
    }

    private void Awake()
    {
        _storage = new Storage();
        _gameData = (GameData) _storage.Load(new GameData());
    }

    private void BuyItem()
    {
        var equipment = _gameData.equipment;
        
        if (equipment.Count < 4)
        {
            equipment.Add(_weapon);
        }

        _gameData.equipment = equipment;

        foreach (var item in _gameData.equipment)
        {
            Debug.Log(item);
        }
        
        _storage.Save(_gameData);
    }
}
