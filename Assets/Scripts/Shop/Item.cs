using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private const string EquippedItemSavePath = "EquippedItem";

    [SerializeField] private string weaponPrefabPath;
    [SerializeField] private Button button;
    [SerializeField] private Text costText;
    [SerializeField] private int cost;
    
    private readonly string[] _equippedSlots = new string[4];
    
    private Inventory _inventory;
    private Bank _bank;

    private void OnEnable()
    {
        button.onClick.AddListener(BuyItem);
        costText.text = cost + "";
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(BuyItem);
    }

    public void Init(Inventory inventory, Bank bank)
    {
        _inventory = inventory;
        _bank = bank;
    }

    private void BuyItem()
    {
        if (_bank.SpendMoney(cost))
        {
            for (int i = 0; i < _equippedSlots.Length; i++)
            {
                var itemPath = PlayerPrefs.GetString(EquippedItemSavePath + i);
                        
                _equippedSlots[i] = itemPath;
            }
                    
            for (int i = _equippedSlots.Length - 1; i >= 1; i--)
            {
                _equippedSlots[i] = _equippedSlots[i - 1];
            }
            
            _equippedSlots[0] = weaponPrefabPath;
            
            for (int i = 0; i < _equippedSlots.Length; i++)
            {
                PlayerPrefs.SetString(EquippedItemSavePath + i, _equippedSlots[i]);
            }
                    
            _inventory.UpdataUI();
        }
    }
}
