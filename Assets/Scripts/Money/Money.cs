using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
	private Vector3 _velocity;
	private float _duration;
	private float _timer;

	private void Update()
	{
		if (_timer < _duration)
		{
			transform.Translate(_velocity * Time.deltaTime, Space.World);
			_timer += Time.deltaTime;
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.collider.TryGetComponent(out Player player))
		{
			AddMoneyToWallet();
			Destroy(gameObject);
		}
	}

	public void SetInitialVelocity(float speed, float spread, float duration)
	{
		Quaternion randomRotation = Quaternion.Euler(0, Random.Range(-spread, spread), 0);
		Vector3 randomDirection = randomRotation * transform.forward;
		_velocity = randomDirection * speed;
		_duration = duration;
		_timer = 0;
	}

	public void AddMoneyToWallet()
	{
		var moneyCount = PlayerPrefs.GetInt("Money", 0);
		moneyCount += 1;
		PlayerPrefs.SetInt("Money", moneyCount);
	}
}
