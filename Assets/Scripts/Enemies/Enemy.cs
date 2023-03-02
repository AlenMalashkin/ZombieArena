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

        public Player Player { get; private set; }

        private Dictionary<Type, IEnemyBehaviour> _behaviours;

        private IEnemyBehaviour _currentBehaviour;
        
        public bool IsDied { get; private set; }

        [Inject]
        private void Construct(Player player, Bank bank)
        {
            Player = player;
            Bank = bank;
        }

        private void OnEnable()
        {
            IsDied = false;
            
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
                [typeof(ChaseEnemyBehaviour)] = new ChaseEnemyBehaviour(agent, Player),
                [typeof(AttackEnemyBehaviour)] = new AttackEnemyBehaviour(Player),
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

            IsDied = true;
        }
}    


