using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ShopInstaller : MonoInstaller
{
	[SerializeField] private Shop shop;
	[SerializeField] private Inventory inventory;
	[SerializeField] private Bank bank;
	[SerializeField] private DisplayMoneyCount displayMoneyCount;
	
	public override void InstallBindings()
	{
		BindShop();
		BindInventory();
		BindBank();
		BindMoneyDisplayer();
	}

	private void BindShop()
	{
		Container
			.Bind<Shop>()
			.FromInstance(shop)
			.AsSingle();
	}

	private void BindInventory()
	{
		Container
			.Bind<Inventory>()
			.FromInstance(inventory)
			.AsSingle();
	}
	
	private void BindBank()
	{
		Container
			.Bind<Bank>()
			.FromInstance(bank)
			.AsSingle();
	}

	private void BindMoneyDisplayer()
	{
		Container
			.Bind<DisplayMoneyCount>()
			.FromInstance(displayMoneyCount)
			.AsSingle();
	}
}
