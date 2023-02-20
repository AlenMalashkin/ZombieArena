using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadStartParams : MonoBehaviour
{
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("EquippedItem0") && !PlayerPrefs.HasKey("EnemyWave"))
        {
            PlayerPrefs.SetString("EquippedItem0", "Weapons/Pistol");
            PlayerPrefs.SetInt("EnemyWave", 1);
        }
    }
}
