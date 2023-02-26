using UnityEngine;

	public class DieEnemyBehaviour : IEnemyBehaviour
	{
		private Collider[] _colliders;
		private Animator _animator;
		private Enemy _enemy;
		
		public DieEnemyBehaviour(Collider[] colliders, Animator animator, Enemy enemy)
		{
			_animator = animator;
			_colliders = colliders;
			_enemy = enemy;
		}
		
		public void Enter()
		{
			foreach (var collider in _colliders)
			{
				collider.enabled = false;
			}
		}

		public void Update()
		{
			_animator.SetBool("Dying", true);
			_animator.SetBool("Punching", false);
			_animator.SetBool("Running", false);
		}

		public void Exit()
		{
			foreach (var collider in _colliders)
			{
				collider.enabled = true;
			}
		}
	}	
