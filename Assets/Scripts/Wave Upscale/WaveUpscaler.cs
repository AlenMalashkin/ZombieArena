using UnityEngine;

public class WaveUpscaler
{
    public int Health
    {
        get
        {
            var waveIndex = PlayerPrefs.GetInt("EnemyWave");
            var health = 10;
            
            for (int i = 0; i < waveIndex; i++)
            {
                if (health < 100)
                    health += 1;
            }

            return health;
        }
    }

    public float Speed
    {
        get
        {
            var waveIndex = PlayerPrefs.GetInt("EnemyWave");
            var speed = 5f;

            for (int i = 0; i < waveIndex; i++)
            {
                if (speed < 10.5f)
                    speed += 0.1f;
            }

            return speed;
        }
    }

    public float AttackRate
    {
        get
        {
            var waveIndex = PlayerPrefs.GetInt("EnemyWave");
            var attackRate = 0.5f;
            
            for (int i = 0; i < waveIndex; i++)
            {
                if (attackRate > 0.1f)
                    attackRate -= 0.02f;
            }
            
            return attackRate;
        }
    }
    
    
}