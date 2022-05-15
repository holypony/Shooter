using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/GameData")]
public class GameSetupSo : ScriptableObject
{
    [SerializeField] private bool isPlay;
    [SerializeField] private float playerHealth;

    public bool IsPlay
    {
        get => isPlay;
        set
        {
            isPlay = value;
            OnIsPlayChange?.Invoke(isPlay);
        }

    }
    
    public float PlayerHealth
    {
        get => playerHealth;
        set
        {
            if (value < 1f) IsPlay = false;
            playerHealth = value;
            OnPlayerHealthChange?.Invoke(playerHealth);
        }

    }
    public event Action<bool> OnIsPlayChange;
    public event Action<float> OnPlayerHealthChange;

}