using UnityEngine;

	public class DieEnemyBehaviour : IEnemyBehaviour
	{
		private GameObject _enemy;
		
		public DieEnemyBehaviour(GameObject enemy)
		{
			_enemy = enemy;
		}
		
		public void Enter()
		{
			Debug.Log("Enemy died");
			_enemy.SetActive(false);
		}

		public void Update()
		{
		}

		public void Exit()
		{
		}
	}	
