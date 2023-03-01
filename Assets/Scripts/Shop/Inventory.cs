using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{ 
    private const string EquippedItemSavePath = "EquippedItem";
    
    
    private const string PistolSavePath = "Weapons/Pistol";
    private const string UziSavePath = "Weapons/Uzi";
    private const string RifleSavePath = "Weapons/Rifle";
    

    [SerializeField] private InventorySlot slotPrefab;
    
    private Dictionary<string, Sprite> _definePrefabPathAsInventorySprite;
    
    private void Awake()
    {
        _definePrefabPathAsInventorySprite = new Dictionary<string, Sprite>()
        {
            {PistolSavePath, Resources.Load<WeaponAbstract>(PistolSavePath).WeaponShopIcon},
            {UziSavePath, Resources.Load<WeaponAbstract>(UziSavePath).WeaponShopIcon},
            {RifleSavePath, Resources.Load<WeaponAbstract>(RifleSavePath).WeaponShopIcon},
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
                
                slot.transform.localScale = new Vector3(1, 1, 1);
                
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
