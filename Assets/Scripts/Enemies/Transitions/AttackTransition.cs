using UnityEngine;

    public class AttackTransition : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;

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
    }

