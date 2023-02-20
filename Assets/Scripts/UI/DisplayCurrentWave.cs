using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DisplayCurrentWave : MonoBehaviour
{
    private Text _text;
    
    private void Awake()
    {
        _text = GetComponent<Text>();

        _text.text = "Текущая волна: " + PlayerPrefs.GetInt("EnemyWave");
    }
}
