using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class EquipmentEnabler : MonoBehaviour
{
	[SerializeField] private float weaponShootingRange;
	
	private List<WeaponAbstract> _initializedWeapons = new List<WeaponAbstract>();
	private EnemyPool _enemyPool;

	public void Construct(EnemyPool enemyPool, List<WeaponAbstract> initializedWeapons)
	{
		_enemyPool = enemyPool;
		_initializedWeapons = initializedWeapons;
	}
	
	private void Update()
	{
		var enemies = _enemyPool.GetActiveEnemies();
		
		var distances = new List<int>();
			
		foreach (var enemy in enemies)
		{ 
			var enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
			distances.Add((int)enemyDistance);
		}

		var minDistance = distances.Min();

		if (enemies.Length > 0 && minDistance < weaponShootingRange)
		{
			foreach (var weapon in _initializedWeapons)
			{
				weapon.gameObject.SetActive(true);
			}
		}
		else
		{
			foreach (var weapon in _initializedWeapons)
			{
				weapon.gameObject.SetActive(false);
			}
		}
	}
}
