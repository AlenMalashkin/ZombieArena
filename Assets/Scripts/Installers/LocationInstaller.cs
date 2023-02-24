using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LocationInstaller : MonoInstaller
{
    [SerializeField] private Player _player;
    [SerializeField] private Bank _bank;
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private Transform _spawnPoint;

    [SerializeField] private EnemyPool _enemyPool;
    [SerializeField] private EquipmentFactory _equipmentFactory;
    
    public override void InstallBindings()
    {
        BindJoystick();
        BindInstallerInterfaces();
        BindEnemyPool();
        BindPlayer();
        BindBank();
    }

    private void BindInstallerInterfaces()
    {
        Container.
            BindInterfacesTo<LocationInstaller>().
            FromInstance(this).
            AsSingle();
    }

    private void BindEnemyPool()
    {
        Container
            .Bind<EnemyPool>()
            .FromInstance(_enemyPool)
            .AsSingle();
    }

    private void BindPlayer()
    {
        var player =
            Container.InstantiatePrefabForComponent<Player>
            (
                _player, 
                _spawnPoint.position, 
                Quaternion.identity,
                null
            );

        Container
            .Bind<Player>()
            .FromInstance(player)
            .AsSingle();
        
        var equipment = Container.
            InstantiatePrefabForComponent<EquipmentFactory>
            (
                _equipmentFactory, 
                player.transform.position, 
                Quaternion.identity, 
                player.transform
            );

        Container
            .Bind<EquipmentFactory>()
            .FromInstance(equipment)
            .AsSingle();
    }

    private void BindBank()
    {
        Container
            .Bind<Bank>()
            .FromInstance(_bank)
            .AsSingle();
    }

    private void BindJoystick()
    {
        Container
            .Bind<FloatingJoystick>()
            .FromInstance(_joystick)
            .AsSingle();
    }
}