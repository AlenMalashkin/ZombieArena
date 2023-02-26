using System;
using UnityEngine;

    public class AttackTransition : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private EnemyHealth _enemyHealth;

        private void OnEnable()
        {
            _enemyHealth.OnEnemyDieEvent += OnEnemyDie;
        }

        private void OnDisable()
        {
            _enemyHealth.OnEnemyDieEvent += OnEnemyDie;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _enemy.SetAttackBehaviour();
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _enemy.SetChaseBehaviour();
            }
        }

        private void OnEnemyDie()
        {
            enabled = false;
        }
    }

