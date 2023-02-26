using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public event Action<float> OnHealthChangedEvent;
	public event Action OnEnemyDieEvent;

	[SerializeField] private Enemy enemy;
	private int _healthDefault;
	
	public float HealthNormalized => (float) Health / _healthDefault;
	private int Health { get; set; }

	private void OnEnable()
	{
		var enemyParams = new WaveUpscaler();
            
		_healthDefault = enemyParams.Health;
            
		Health = _healthDefault;
		OnHealthChangedEvent?.Invoke(HealthNormalized);
	}
	
	private void OnCollisionEnter(Collision other)
	{
		if (other.collider.TryGetComponent(out BulletDefault bullet))
		{
			HitEnemy(bullet.damage);  
		}
	}
	
	private void HitEnemy(int damage)
	{
		Health -= damage;

		if (Health <= 0)
		{
			enemy.SetDieBehaviour();
			OnEnemyDieEvent?.Invoke();
		}

		OnHealthChangedEvent?.Invoke(HealthNormalized);
	}

	private void DisableEnemy()
	{
		gameObject.SetActive(false);
	}
}
