using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;


[RequireComponent(typeof(NavMeshAgent))] 
public class Enemy : MonoBehaviour 
{
        public event Action<float> OnHealthChangedEvent;
        public event Action OnEnemyDieEvent;

        private const string RunAnimationName = "Running";
        private const string PunchAnimationName = "Punching";
        private const string DieAnimationName = "Dying";

        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private int healthDefault;
        [SerializeField] private Animator animator;
        
        public Bank Bank { get; private set; }
        
        public float healthNormalized => (float) health / healthDefault;
        private int health { get; set; }
        

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
            
            healthDefault = enemyParams.Health;
            agent.speed = enemyParams.Speed;
            
            health = healthDefault;
            OnHealthChangedEvent?.Invoke(healthNormalized);
        }

        private void Update()
        {
            if (_currentBehaviour != null)
                _currentBehaviour.Update();
            
            Debug.Log(_currentBehaviour);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.collider.TryGetComponent(out BulletDefault bullet))
            {
                HitEnemy(bullet.damage);  
            }
        }

        private void HitEnemy(int damage)
        {
            health -= damage;

            if (health <= 0)
            {
                OnEnemyDieEvent?.Invoke();
                gameObject.SetActive(false);
            }

            OnHealthChangedEvent?.Invoke(healthNormalized);
        }

        private void InitBehaviours()
        {
            _behaviours = new Dictionary<Type, IEnemyBehaviour>
            {
                [typeof(ChaseEnemyBehaviour)] = new ChaseEnemyBehaviour(agent, _player),
                [typeof(AttackEnemyBehaviour)] = new AttackEnemyBehaviour(_player),
                [typeof(DieEnemyBehaviour)] = new DieEnemyBehaviour(gameObject)
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
        }

        public void SetAttackBehaviour()
        {
            var behaviour = GetBehaviour<AttackEnemyBehaviour>();
            SetBehaviour(behaviour);
            animator.SetBool(PunchAnimationName, true);
        }


        public void SetDieBehaviour()
        {
            var behaviour = GetBehaviour<DieEnemyBehaviour>();
            SetBehaviour(behaviour);
            animator.SetBool(DieAnimationName, true);
        }
}    


