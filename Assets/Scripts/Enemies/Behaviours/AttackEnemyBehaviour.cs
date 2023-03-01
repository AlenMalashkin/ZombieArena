using UnityEngine;

public class AttackEnemyBehaviour : IEnemyBehaviour
{
		private Player _player;
		private float _attackRate;
		private Timer _timer;
		
		public AttackEnemyBehaviour(Player player)
		{
			_player = player;
			_attackRate = new WaveUpscaler().AttackRate;
		}
		
		public void Enter()
		{
			_timer = new Timer(TimerType.UpdateTick);
			
			_timer.Start(_attackRate);
			_timer.TimerFinished += HitPlayer;
		}

		public void Update()
		{
		}

		public void Exit()
		{
			_timer.TimerFinished -= HitPlayer;
		}

		private void HitPlayer()
		{
			_player.HitPlayer(1);
			_timer.Start(_attackRate);
		}
}	

