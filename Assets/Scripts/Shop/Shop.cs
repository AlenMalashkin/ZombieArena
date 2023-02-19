using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private Item[] _items;
    [SerializeField] private Inventory _inventory;

    private void Awake()
    {
        foreach (var item in _items)
        {
            var itemInstance = Instantiate(item, transform, true);
            itemInstance.Init(_inventory);
        }
    }
}
