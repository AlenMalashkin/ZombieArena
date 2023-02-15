using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image _image;

    public void Init(Sprite itemSprite)
    {
        _image.sprite = itemSprite;
    }
}
