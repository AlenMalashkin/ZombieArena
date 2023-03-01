using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public abstract class WeaponAbstract : MonoBehaviour
{
	[Header("Shop imagination")] 
	[SerializeField] private Sprite weaponShopIcon;
	
	public Sprite WeaponShopIcon => weaponShopIcon;
	
	private EnemyPool _enemyPool;
	
	public void Construct(EnemyPool enemyPool)
	{
		_enemyPool = enemyPool;
	}

	public void LookAtTarget()
	{
		var enemies = _enemyPool.GetActiveEnemies();

		if (enemies.Length != 0)
		{
			var distances = new List<int>();
            		
			foreach (var enemy in enemies)
			{ 
				var enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
				distances.Add((int)enemyDistance);
			}

			var enemyWithMinDistanceToPlayer = enemies[Array.IndexOf(distances.ToArray(), distances.Min())];
			
			if (!enemyWithMinDistanceToPlayer.IsDied)
				transform.LookAt(enemyWithMinDistanceToPlayer.transform);
		}
	}
}
