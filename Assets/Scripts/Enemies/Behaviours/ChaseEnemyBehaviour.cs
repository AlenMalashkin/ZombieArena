using UnityEngine.AI;


	public class ChaseEnemyBehaviour : IEnemyBehaviour
	{
		private NavMeshAgent _agent;
		private Player _player;

		public ChaseEnemyBehaviour(NavMeshAgent agent, Player player)
		{
			_agent = agent;
			_player = player;
		}
		
		public void Enter()
		{
			_agent.enabled = true;
		}

		public void Update()
		{
			_agent.SetDestination(_player.transform.position);
		}

		public void Exit()
		{
			_agent.enabled = false;
		}
	}	

