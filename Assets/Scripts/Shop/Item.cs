using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public const string EquippedItemSavePath = "EquippedItem";
    
    [SerializeField] private string _weaponPrefabPath;
    [SerializeField] private Button _button;

    private void OnEnable()
    {
        _button.onClick.AddListener(BuyItem);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(BuyItem);
    }

    private void BuyItem()
    {
        var equippedSlots = new string[4];

        for (int i = 0; i < 4; i++)
        {
            var itemPath = PlayerPrefs.GetString(EquippedItemSavePath + i);
            equippedSlots[i] = itemPath;
        }

        for (int i = equippedSlots.Length - 1; i >= 1; i--)
        {
            equippedSlots[i] = equippedSlots[i - 1];
        }

        equippedSlots[0] = _weaponPrefabPath;
        
        foreach (var slot in equippedSlots)
        {
            Debug.Log(slot);
        }
    }
}
