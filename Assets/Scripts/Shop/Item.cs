using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public const string EquippedItemSavePath = "EquippedItem";

    [SerializeField] private Inventory _inventory;
    [SerializeField] private string _weaponPrefabPath;
    [SerializeField] private Button _button;
    private string[] equippedSlots = new string[4];

    private void OnEnable()
    {
        _button.onClick.AddListener(BuyItem);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(BuyItem);
    }

    public void Init(Inventory inventory)
    {
        _inventory = inventory;
    }

    private void BuyItem()
    { 
        for (int i = 0; i < equippedSlots.Length; i++)
        {
            var itemPath = PlayerPrefs.GetString(EquippedItemSavePath + i);
            
            equippedSlots[i] = itemPath;
        }
        
        for (int i = equippedSlots.Length - 1; i >= 1; i--)
        {
            equippedSlots[i] = equippedSlots[i - 1];
        }

        equippedSlots[0] = _weaponPrefabPath;

        for (int i = 0; i < equippedSlots.Length; i++)
        {
            PlayerPrefs.SetString(EquippedItemSavePath + i, equippedSlots[i]);
        }
        
        _inventory.UpdataUI();
    }
}
