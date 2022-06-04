using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/GameData")]
public class GameSetupSo : ScriptableObject
{
    [SerializeField] private bool isPlay;
    [SerializeField] private bool isSound;
    [SerializeField] private bool isRocketLauncher;
    [SerializeField] private int health;
    [SerializeField] private int bullets;
    [SerializeField] private int rockets;
    [SerializeField] private int kills;
    [SerializeField] private int truck;

    public bool IsPlay
    {
        get => isPlay;
        set
        {
            isPlay = value;
            OnIsPlayChange?.Invoke(isPlay);
        }
    }

    public bool IsRocketLauncher
    {
        get => isRocketLauncher;
        set
        {
            isRocketLauncher = value;
            OnIsRocketLauncherChange?.Invoke(isRocketLauncher);
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

    public int Rockets
    {
        get => rockets;
        set
        {
            rockets = value;
            OnRocketsChange?.Invoke(rockets);
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

    public int Truck
    {
        get => truck;
        set
        {
            truck = value;
            OnTruckChange?.Invoke(truck);
        }
    }


    public event Action<bool> OnIsPlayChange;
    public event Action<bool> OnIsSoundChange;
    public event Action<int> OnHealthChange;
    public event Action<int> OnBulletsChange;
    public event Action<int> OnKillsChange;

    public event Action<bool> OnIsRocketLauncherChange;

    public event Action<int> OnRocketsChange;
    public event Action<int> OnTruckChange;
}