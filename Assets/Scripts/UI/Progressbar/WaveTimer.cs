using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveTimer : MonoBehaviour
{
    [SerializeField] private ProgressBar _progressBar;
    [SerializeField] private float _timeRemain = 120;

    private Timer _timer;

    private void OnEnable()
    {
        _timer = new Timer(TimerType.OneSecTick);
        
        _timer.Start(_timeRemain);
        
        _timer.TimerFinished += TimerFinished;
        _timer.TimerValueChanged += TimerValueChanged;
    }

    private void OnDisable()
    {
        _timer.TimerFinished -= TimerFinished;
        _timer.TimerValueChanged -= TimerValueChanged;
    }

    private void TimerFinished()
    {
        var currentWave = PlayerPrefs.GetInt("EnemyWave", 1);
        currentWave += 1;
        PlayerPrefs.SetInt("EnemyWave",currentWave);

        SceneManager.LoadScene("WavePassed");
    }

    private void TimerValueChanged(float remainSeconds, TimeChangingSource changingSource)
    {
        _progressBar.SetTimeValue(remainSeconds, _timeRemain);
    }
}
