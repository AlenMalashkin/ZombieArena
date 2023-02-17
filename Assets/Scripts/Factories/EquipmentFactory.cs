using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class EquipmentFactory : MonoBehaviour
{
	public const string EquippedItemSavePath = "EquippedItem";
	
	[SerializeField] private EquipmentEnabler _equipmentEnabler;
	[SerializeField] private Transform[] _gunInitPositions;
	[SerializeField] private GameData _gameData;
	
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
		if (!PlayerPrefs.HasKey(EquippedItemSavePath + 0))
		{
			PlayerPrefs.SetString(EquippedItemSavePath + 0, "Weapons/Pistol");
		}
		
		_equipmentEnabler.Construct(_enemyPool, InitializedWeapons);
		
		var weapons = new List<WeaponAbstract>();

		for (int i = 0; i < 4; i++)
		{
			if (PlayerPrefs.HasKey(EquippedItemSavePath + i))
			{
				weapons.Add(Resources.Load<WeaponAbstract>(PlayerPrefs.GetString(EquippedItemSavePath + i)));
			}
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
