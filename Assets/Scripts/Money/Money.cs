using UnityEngine;
using Random = UnityEngine.Random;

public class Money : MonoBehaviour
{
    [SerializeField] private float attractSpeed = 5f;
    private Player _player;

	private Bank _bank;
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
		
		Vector3 directionToPlayer = (_player.transform.position - transform.position).normalized;
        
        transform.position += directionToPlayer * attractSpeed * Time.deltaTime;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out Player player))
		{
			_bank.GetMoney(1);
			Destroy(gameObject);
		}
	}

	public void Init(float speed, float spread, float duration, Bank bank, Player player)
	{
		_bank = bank;
		_player = player;
		
		Quaternion randomRotation = Quaternion.Euler(0, Random.Range(-spread, spread), 0);
		Vector3 randomDirection = randomRotation * transform.forward;
		_velocity = randomDirection * speed;
		_duration = duration;
		_timer = 0;
	}
}
