using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	private IControllable _controllable;
	private GameInput _gameInput;
	private FloatingJoystick _joystick;
	
	private void Awake()
	{
		_gameInput = new GameInput();
		_gameInput.Enable();
		
		var player = GetComponent<Player>();
		_controllable = player;
		_joystick = player.Joystick;
		
		if (_controllable == null)
		{
			throw new Exception("There is no IControllable component");
		}
	}
	
	private void Update()
	{
		Vector3 direction;

		if (
			_joystick.Horizontal > 0
		    || _joystick.Vertical > 0
		    || _joystick.Horizontal < 0
		    || _joystick.Vertical < 0
			)
		{
			direction = ReadVirtualJoystickMove();
		}
		else
		{
			direction = ReadKeyboardMove();
		}

		_controllable.Move(direction);
	}

	private Vector3 ReadKeyboardMove()
	{
		var input = _gameInput.Gameplay.Movement.ReadValue<Vector2>();

		return new Vector3(input.x, 0f, input.y);
	}

	private Vector3 ReadVirtualJoystickMove()
	{
		return new Vector3(_joystick.Horizontal, 0, _joystick.Vertical);
	}
}
