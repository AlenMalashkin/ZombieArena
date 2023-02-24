using UnityEngine;
using Zenject;

public class Shop : MonoBehaviour
{
    [SerializeField] private Item[] _items;
    
    private Inventory _inventory;
    private Bank _bank;

    [Inject]
    private void Construct(Inventory inventory, Bank bank)
    {
        _inventory = inventory;
        _bank = bank;
    }

    private void Awake()
    {
        foreach (var item in _items)
        {
            var itemInstance = Instantiate(item, transform, true);
            itemInstance.Init(_inventory, _bank);
        }
    }
}
