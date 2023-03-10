using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class DropMoneyAfterDeath : MonoBehaviour
{
	[SerializeField] private Enemy enemy;
	[SerializeField] private EnemyHealth enemyHealth;
	[SerializeField] private Money moneyPrefab;

	[SerializeField] private int moneyAmount = 5;
	[SerializeField] private float moneySpeed = 2f;
	[SerializeField] private float moneySpread = 180f;
	[SerializeField] private float minMoveDuration = 1;
	[SerializeField] private float maxMoveDuration = 2;

	private void OnEnable()
	{
		enemyHealth.OnEnemyDieEvent += DropMoney;
	}

	private void OnDisable()
	{
		enemyHealth.OnEnemyDieEvent -= DropMoney;
	}

	private void DropMoney()
	{
		for (int i = 0; i < moneyAmount; i++)
		{
			Money money = Instantiate(moneyPrefab, transform.position, Quaternion.identity);
			money.Init(moneySpeed, moneySpread, Random.Range(minMoveDuration, maxMoveDuration), enemy.Bank, enemy.Player);
		}
	}
}
