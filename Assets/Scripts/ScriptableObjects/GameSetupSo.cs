using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/GameData")]
public class GameSetupSo : ScriptableObject
{
    [SerializeField] private bool isPlay;
    [SerializeField] private bool isSound;
    [SerializeField] private int health;
    [SerializeField] private int bullets;
    [SerializeField] private int kills;
    
    public bool IsPlay
    {
        get => isPlay;
        set
        {
            isPlay = value;
            OnIsPlayChange?.Invoke(isPlay);
        }
    }
    
    public bool IsSound
    {
        get => isSound;
        set
        {
            isSound = value;
            OnIsSoundChange?.Invoke(isSound);
        }
    }
    
    public int Health
    {
        get => health;
        set
        {
            health = value;
            OnHealthChange?.Invoke(health);
        }
    } 
    
    public int Bullets
    {
        get => bullets;
        set
        {
            bullets = value;
            OnBulletsChange?.Invoke(bullets);
        }
    }
    
    public int Kills
    {
        get => kills;
        set
        {
            kills = value;
            OnKillsChange?.Invoke(kills);
        }
    }


    public event Action<bool> OnIsPlayChange;
    public event Action<bool> OnIsSoundChange;
    public event Action<int> OnHealthChange;
    public event Action<int> OnBulletsChange;
    public event Action<int> OnKillsChange;
}