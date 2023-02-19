using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LifeBar : MonoBehaviour
{
    [SerializeField] private ProgressBar _progressBar;

    private Player _player;

    [Inject]
    private void Construct(Player player)
    {
        _player = player;
    }

    private void OnEnable()
    {
        _progressBar.SetValue(_player.healthNormalized);
        
        _player.OnHealthChangedEvent += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.OnHealthChangedEvent -= OnHealthChanged;
    }

    private void OnHealthChanged(float newValue)
    {
        _progressBar.SetValue(newValue);
    }
}
