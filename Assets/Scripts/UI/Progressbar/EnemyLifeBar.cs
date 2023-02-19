
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLifeBar : MonoBehaviour
{
    [SerializeField] private ProgressBar _progressBar;
    [SerializeField] private Enemy _enemy;

    private void OnEnable()
    {
        _progressBar.SetValue(_enemy.healthNormalized);
        
        _enemy.OnHealthChangedEvent += OnHealthChanged;
    }

    private void OnDisable()
    {
        _enemy.OnHealthChangedEvent -= OnHealthChanged;
    }

    private void OnHealthChanged(float newValue)
    {
        _progressBar.SetValue(newValue);
    }
}
