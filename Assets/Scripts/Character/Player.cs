using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour, IControllable
{
	public event Action<float> OnHealthChangedEvent;
	
	[SerializeField] private float _speed;
	[SerializeField] private float _gravity;
	[SerializeField] private Transform _groundCheckerPosition;
	[SerializeField] private LayerMask _groundMask;
	[SerializeField] private float _checkGroundSphereRadius;
	[SerializeField] private Animator animator;
	
	[SerializeField] private int _healthDefault = 100;

	public FloatingJoystick Joystick { get; private set; }
	private int _health;
	public float healthNormalized => (float) _health / _healthDefault;
	
	private CharacterController _controller;
	private float _velocity;
	private Vector3 _moveDirection;

	[Inject]
	private void Construct(FloatingJoystick joystick)
	{
		Joystick = joystick;
	}
	
	private void Awake()
	{
		_controller = GetComponent<CharacterController>();
		_health = _healthDefault;
	}

	private void FixedUpdate()
	{
		MoveInternal();
		DoGravity();

		if (IsGrounded())
		{
			_velocity = -2;
		}
		
		if (_moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(_moveDirection);
        }

		animator.SetFloat("Speed", _moveDirection.magnitude);
	}

	public void Move(Vector3 direction)
	{
		_moveDirection = direction;
	}

	private void MoveInternal()
	{
		_controller.Move(_moveDirection * _speed * Time.fixedDeltaTime);
	}

	private bool IsGrounded()
	{
		bool result = Physics.CheckSphere(_groundCheckerPosition.position, _checkGroundSphereRadius, _groundMask);

		return result;
	}

	private void DoGravity()
	{
		_velocity += _gravity * Time.fixedDeltaTime;

		_controller.Move(Vector3.up * _velocity * Time.fixedDeltaTime);
	}

	public void HitPlayer(int damage)
	{
		if (_health <= 0)
		{
			SceneManager.LoadScene("WaveDefeated");
		}
		
		_health -= damage;
		
		OnHealthChangedEvent?.Invoke(healthNormalized);

	}
}
