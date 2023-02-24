using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class DisplayMoneyCount : MonoBehaviour
{
    [SerializeField] private Text text;

    private Bank _bank;
    
    [Inject]
    private void Construct(Bank bank)
    {
        _bank = bank;
    }

    private void OnEnable()
    {
        _bank.OnMoneyCountChangedEvent += SetCurrentMoneyCount;
    }

    private void OnDisable()
    {
        _bank.OnMoneyCountChangedEvent -= SetCurrentMoneyCount;
    }

    private void Awake()
    {
        SetCurrentMoneyCount(PlayerPrefs.GetInt("Money", 0));
    }

    private void SetCurrentMoneyCount(int moneyCount)
    {
        text.text = moneyCount + "";
    }
}
