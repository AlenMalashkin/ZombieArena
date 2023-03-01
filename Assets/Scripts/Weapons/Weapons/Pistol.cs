using UnityEngine;

public class Pistol : WeaponAbstract
{ 
	[Header("Weapon params")]
	[SerializeField] private PistolBullet bullet;
	[SerializeField] private float fireRate;

	private PoolMono<PistolBullet> _bulletPool;
	
	private Timer _timer;
	
	private void OnEnable()
	{
		_timer = new Timer(TimerType.UpdateTick);
		_timer.TimerFinished += Shoot;
		Shoot();
	}

	private void OnDisable()
	{
		_timer.TimerFinished -= Shoot;
	}
	
	private void Awake()
	{
		_bulletPool = new PoolMono<PistolBullet>(bullet, 30, transform);
		_bulletPool.AutoExpand = true;
	}
	
	private void Update()
	{
		LookAtTarget();
	}

	private void Shoot()
	{
		var bullet = _bulletPool.GetFreeElement();
		bullet.transform.position = transform.position;
		_timer.Start(fireRate);
	}
}
