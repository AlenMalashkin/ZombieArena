using System;
using UnityEngine;

public class BulletDefault : MonoBehaviour
{
	[SerializeField] private float _lifeTime;
	[SerializeField] private float _bulletSpeed;
	[SerializeField] private float _damage;

	public float damage => _damage;
	private Timer _timer;

	private void OnEnable()
	{
		_timer = new Timer(TimerType.OneSecTick);

		_timer.Start(_lifeTime);

		_timer.TimerFinished += () => gameObject.SetActive(false);
	}

	private void OnDisable()
	{
		_timer = null;
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.collider.TryGetComponent(out Enemy enemy))
		{
			gameObject.SetActive(false);
		}
	}

	private void Update()
	{
		transform.Translate(Vector3.forward * _bulletSpeed * Time.deltaTime);
	}
}