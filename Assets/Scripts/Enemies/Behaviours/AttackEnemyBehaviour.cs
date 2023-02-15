using System.Collections;
using UnityEngine;
using UnityEngine.AI;


	public class AttackEnemyBehaviour : IEnemyBehaviour
	{
		private float _startAttackRate;
		private float _attackRate;
		
		public AttackEnemyBehaviour(float startAttackRate)
		{
			_startAttackRate = startAttackRate;
		}
		
		public void Enter()
		{
			_attackRate = _startAttackRate;
		}

		public void Update()
		{
			_attackRate -= Time.deltaTime;

			if (_attackRate <= 0)
			{
				Debug.Log("Player hited");
				_attackRate = _startAttackRate;
			}
		}

		public void Exit()
		{
		}
	}	

