using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;


    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        public event Action<float> OnHealthChangedEvent;
        
        [SerializeField] private float _attackRate;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private int _healthDefault;

        private int health { get; set; }
        public float healthNormalized => (float) health / _healthDefault;
        
        private Player _player;

        private Dictionary<Type, IEnemyBehaviour> _behaviours;

        private IEnemyBehaviour _currentBehaviour;
        
        [Inject]
        private void Construct(Player player)
        {
            _player = player;
        }

        private void OnEnable()
        {
            InitBehaviours();
            SetBehaviourByDefault();
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
                gameObject.SetActive(false);

            OnHealthChangedEvent?.Invoke(healthNormalized);
        }

        private void InitBehaviours()
        {
            _behaviours = new Dictionary<Type, IEnemyBehaviour>
            {
                [typeof(ChaseEnemyBehaviour)] = new ChaseEnemyBehaviour(_agent, _player),
                [typeof(AttackEnemyBehaviour)] = new AttackEnemyBehaviour(_player, _attackRate),
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

        public void SetEnemyStatsOnCurrentWave(int healthCount, float speed, float attackRate)
        {
            _healthDefault = healthCount;
            health = _healthDefault;
            _agent.speed = speed;
            _attackRate = attackRate;
        }

        public void SetChaseBehaviour()
        {
            var behaviour = GetBehaviour<ChaseEnemyBehaviour>();
            SetBehaviour(behaviour);
        }

        public void SetAttackBehaviour()
        {
            var behaviour = GetBehaviour<AttackEnemyBehaviour>();
            SetBehaviour(behaviour);
        }


        public void SetDieBehaviour()
        {
            var behaviour = GetBehaviour<DieEnemyBehaviour>();
            SetBehaviour(behaviour);
        }
    }    


