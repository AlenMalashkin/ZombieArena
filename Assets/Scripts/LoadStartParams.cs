using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadStartParams : MonoBehaviour
{
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("EquippedItem0"))
            PlayerPrefs.SetString("EquippedItem0", "Weapons/Pistol");
        
        if (!PlayerPrefs.HasKey("EnemyWave"))
            PlayerPrefs.SetInt("EnemyWave", 1);
    }
}
