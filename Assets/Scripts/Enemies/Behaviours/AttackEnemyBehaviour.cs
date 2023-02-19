using System.Collections;
using UnityEngine;
using UnityEngine.AI;


	public class AttackEnemyBehaviour : IEnemyBehaviour
	{
		private Player _player;
		private float _startAttackRate;
		private float _attackRate;
		
		public AttackEnemyBehaviour(Player player, float startAttackRate)
		{
			_startAttackRate = startAttackRate;
			_player = player;
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
				_player.HitPlayer(5);
				_attackRate = _startAttackRate;
			}
		}

		public void Exit()
		{
		}
	}	

