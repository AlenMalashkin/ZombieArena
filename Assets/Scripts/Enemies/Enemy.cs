using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;


    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {

        [SerializeField] private float _health;
        [SerializeField] private float _attackRate;
        [SerializeField] private NavMeshAgent _agent;
        
        public float health => _health;
        
        
        private Player _player;
        public Player Player => _player;

        private Dictionary<Type, IEnemyBehaviour> _behaviours;

        private IEnemyBehaviour _currentBehaviour;
        
        [Inject]
        private void Construct(Player player)
        {
            _player = player;
        }

        private void OnEnable()
        {
            _health = 10;
            InitBehaviours();
            SetBehaviourByDefault();
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
                Debug.Log("Enemy hited!");
                _health -= bullet.damage;
                bullet.gameObject.SetActive(false);

                if (health <= 0)
                    gameObject.SetActive(false);   
            }
        }

        private void InitBehaviours()
        {
            _behaviours = new Dictionary<Type, IEnemyBehaviour>
            {
                [typeof(ChaseEnemyBehaviour)] = new ChaseEnemyBehaviour(_agent, _player),
                [typeof(AttackEnemyBehaviour)] = new AttackEnemyBehaviour(_attackRate),
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


