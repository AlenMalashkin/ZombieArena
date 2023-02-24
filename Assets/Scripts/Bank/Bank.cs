using System;
using UnityEngine;

public class Bank : MonoBehaviour
{
	public event Action<int> OnMoneyCountChangedEvent;
	
	public bool SpendMoney(int amount)
	{
		var currentMoneyCount = PlayerPrefs.GetInt("Money", 0);
		
		if (currentMoneyCount >= amount)
		{
			currentMoneyCount -= amount;
			OnMoneyCountChangedEvent?.Invoke(currentMoneyCount);
			PlayerPrefs.SetInt("Money", currentMoneyCount);
			return true;
		}

		return false;
	}

	public void GetMoney(int amount)
	{
		var currentMoneyCount = PlayerPrefs.GetInt("Money", 0);
		currentMoneyCount += amount;
		OnMoneyCountChangedEvent?.Invoke(currentMoneyCount);
		PlayerPrefs.SetInt("Money", currentMoneyCount);
	}
}
