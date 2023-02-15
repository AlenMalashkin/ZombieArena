using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour, IControllable
{
	[SerializeField] private float _speed;
	[SerializeField] private float _gravity;
	[SerializeField] private Transform _groundCheckerPosition;
	[SerializeField] private LayerMask _groundMask;
	[SerializeField] private float _checkGroundSphereRadius;
	
	private CharacterController _controller;
	private float _velocity;
	private Vector3 _moveDirection;

	private void Awake()
	{
		_controller = GetComponent<CharacterController>();
	}

	private void FixedUpdate()
	{
		MoveInternal();
		DoGravity();

		if (IsGrounded())
		{
			_velocity = -2;
		}
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
}
