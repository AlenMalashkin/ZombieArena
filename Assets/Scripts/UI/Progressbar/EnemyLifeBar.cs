
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeBar : MonoBehaviour
{
    [SerializeField] private ProgressBar _progressBar;
    [SerializeField] private EnemyHealth _enemyHealth;

    private void OnEnable()
    {
        _progressBar.SetValue(_enemyHealth.HealthNormalized);
        
        _enemyHealth.OnHealthChangedEvent += OnHealthChanged;
    }

    private void OnDisable()
    {
        _enemyHealth.OnHealthChangedEvent -= OnHealthChanged;
    }

    private void OnHealthChanged(float newValue)
    {
        _progressBar.SetValue(newValue);
    }
}
