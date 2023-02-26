using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;


[RequireComponent(typeof(NavMeshAgent))] 
public class Enemy : MonoBehaviour 
{
        private const string RunAnimationName = "Running";
        private const string PunchAnimationName = "Punching";
        private const string DieAnimationName = "Dying";

        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Animator animator;
        [SerializeField] private Collider[] collidersToEnable;

        public Bank Bank { get; private set; }
        
        private Player _player;

        private Dictionary<Type, IEnemyBehaviour> _behaviours;

        private IEnemyBehaviour _currentBehaviour;
        
        [Inject]
        private void Construct(Player player, Bank bank)
        {
            _player = player;
            Bank = bank;
        }

        private void OnEnable()
        {
            InitBehaviours();
            SetBehaviourByDefault();
            
            var enemyParams = new WaveUpscaler();
            
            agent.speed = enemyParams.Speed;
        }

        private void Update()
        {
            if (_currentBehaviour != null)
                _currentBehaviour.Update();
        }

        private void InitBehaviours()
        {
            _behaviours = new Dictionary<Type, IEnemyBehaviour>
            {
                [typeof(ChaseEnemyBehaviour)] = new ChaseEnemyBehaviour(agent, _player),
                [typeof(AttackEnemyBehaviour)] = new AttackEnemyBehaviour(_player),
                [typeof(DieEnemyBehaviour)] = new DieEnemyBehaviour(collidersToEnable, animator, this)
            };
        }

        private void SetBehaviourByDefault()
        {
            var defaultBehaviour = GetBehaviour<ChaseEnemyBehaviour>();
            SetBehaviour(defaultBehaviour);
        }

        private void SetBehaviour(IEnemyBehaviour behaviour)
        {
            if (_currentBehaviour != null)
                _currentBehaviour.Exit();

            _currentBehaviour = behaviour;
            _currentBehaviour.Enter();
        }

        private IEnemyBehaviour GetBehaviour<T>() where T : IEnemyBehaviour
        {
            return _behaviours[typeof(T)];
        }

        public void SetChaseBehaviour()
        {
            var behaviour = GetBehaviour<ChaseEnemyBehaviour>();
            SetBehaviour(behaviour);
            animator.SetBool(RunAnimationName, true);
            animator.SetBool(PunchAnimationName, false);
            animator.SetBool(DieAnimationName, false);
        }

        public void SetAttackBehaviour()
        {
            var behaviour = GetBehaviour<AttackEnemyBehaviour>();
            SetBehaviour(behaviour);
            animator.SetBool(RunAnimationName, false);
            animator.SetBool(PunchAnimationName, true);
            animator.SetBool(DieAnimationName, false);
        }


        public void SetDieBehaviour()
        {
            var behaviour = GetBehaviour<DieEnemyBehaviour>();
            SetBehaviour(behaviour);
            animator.SetBool(RunAnimationName, false);
            animator.SetBool(PunchAnimationName, false);
            animator.SetBool(DieAnimationName, true);
        }
}    


