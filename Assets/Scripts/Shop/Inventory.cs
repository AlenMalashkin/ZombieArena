using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{ 
    public const string EquippedItemSavePath = "EquippedItem";

    [SerializeField] private InventorySlot slotPrefab;
    [SerializeField] private List<Sprite> inventorySlotSprites;
    
    private Dictionary<string, Sprite> _definePrefabPathAsInventorySprite;
    
    private void Awake()
    {
        _definePrefabPathAsInventorySprite = new Dictionary<string, Sprite>()
        {
            {"Weapons/Pistol", inventorySlotSprites[0]},
            {"Weapons/Uzi", inventorySlotSprites[1]},
            {"Weapons/Rifle", inventorySlotSprites[2]},
        };
        
        LoadItemIcons();
    }

    private void LoadItemIcons()
    {
        for (int i = 0; i < 4; i++)
        {
            if (PlayerPrefs.GetString(EquippedItemSavePath + i) != "")
            {
                var slot = Instantiate(slotPrefab, transform, true);
                        
                slot.Init(_definePrefabPathAsInventorySprite
                    [PlayerPrefs.GetString(EquippedItemSavePath + i)]);
            }
        }
    }

    private void RemoveItemIcons()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject); 
        }
    }
    
    public void UpdataUI()
    {
        RemoveItemIcons();
        LoadItemIcons();
    }
}
