using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class EquipmentFactory : MonoBehaviour
{
	[SerializeField] private EquipmentEnabler _equipmentEnabler;
	[SerializeField] private Transform[] _gunInitPositions;
	[SerializeField] private GameData _gameData;

	private Storage _storage;
	
	public Transform[] GunInitPositions => _gunInitPositions;
	public List<WeaponAbstract> InitializedWeapons { get; } = new List<WeaponAbstract>();

	private EnemyPool _enemyPool;
	
	[Inject]
	private void Construct(EnemyPool enemyPool)
	{
		_enemyPool = enemyPool;
	}

	private void Awake()
	{
		_equipmentEnabler.Construct(_enemyPool, InitializedWeapons);
		
		Debug.Log(_gameData.equipment);
		
		_storage = new Storage();
		_gameData = (GameData) _storage.Load(_gameData);
		
		var weapons = _gameData.equipment;
		
		Debug.Log(weapons);
		
		foreach (var weapon in weapons)
		{
			Debug.Log(weapon);
		}

		for (int i = 0; i < weapons.Count; i++)
		{
			var gun = Create(weapons[i], GunInitPositions[i]);
			InitializedWeapons.Add(gun);
		}
	}

	public WeaponAbstract Create(WeaponAbstract weapon, Transform at)
	{
		var gun = Instantiate
			(
				weapon,
				at.position,
				Quaternion.identity, 
				transform
			);

		gun.Construct(_enemyPool);
		
		return gun;
	}
}
