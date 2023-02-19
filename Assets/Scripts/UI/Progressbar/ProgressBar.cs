using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image _filler;
    [SerializeField] private Text _textValue;
    
    public void SetValue(float valueNormalized)
    {
        _filler.fillAmount = valueNormalized;

        var valueInPercent = Mathf.RoundToInt(valueNormalized * 100f);
        _textValue.text = $"{valueInPercent}%";
    }

    public void SetTimeValue(float timeLeft, float startTime)
    {
        float minutes = Mathf.FloorToInt(timeLeft / 60);
        float seconds = Mathf.FloorToInt(timeLeft % 60);
        _textValue.text = $"{minutes:00} : {seconds:00}";
        var normalizedValue = Mathf.Clamp(timeLeft / startTime, 0.0f, 1.0f);
        _filler.fillAmount = normalizedValue;
    }
}
