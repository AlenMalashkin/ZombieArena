using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	private IControllable _controllable;
	private GameInput _gameInput;

	private void Awake()
	{
		_gameInput = new GameInput();
		_gameInput.Enable();
		
		_controllable = GetComponent<IControllable>();

		if (_controllable == null)
		{
			throw new Exception("There is no IControllable component");
		}
	}
	
	private void Update()
	{
		ReadMove();
	}

	private void ReadMove()
	{
		var input = _gameInput.Gameplay.Movement.ReadValue<Vector2>();

		var direction = new Vector3(input.x, 0f, input.y);
		
		_controllable.Move(direction);
	}
}
