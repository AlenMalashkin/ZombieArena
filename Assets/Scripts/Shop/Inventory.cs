using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Sprite[] _itemSprites;
    [SerializeField] private InventorySlot _slot;

    private Dictionary<Type, Sprite> _defineItemSpriteByType;

    private Storage _storage;
    private GameData _gameData;

    private void Awake()
    {
        _storage = new Storage();
        _gameData = (GameData) _storage.Load(new GameData());
        
        _defineItemSpriteByType = new Dictionary<Type, Sprite>()
        {
            {typeof(Pistol), _itemSprites[0]},
        };

        for (int i = 0; i < _gameData.equipment.Count; i++)
        {
            var slot = Instantiate(_slot, transform, true);
            slot.Init(_defineItemSpriteByType[_gameData.equipment[i].GetType()]);
        }
    }
}
