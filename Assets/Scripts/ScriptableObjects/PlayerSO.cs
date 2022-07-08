using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/PlayerData")]
public class PlayerSO : ScriptableObject
{
    [SerializeField] private int health;

    [SerializeField] private int kills;
    [SerializeField] private int lvl;
    [SerializeField] private float xp;


    [SerializeField] private int bullets;
    [SerializeField] private int rockets;
    [SerializeField] private int crystal;

    public event Action<int> OnHealthChange;
    public event Action<int> OnLevelChange;
    public event Action<float> OnXPChange;
    public event Action<int> OnKillsChange;

    public event Action<int> OnBulletsChange;
    public event Action<int> OnRocketsChange;
    public event Action<int> OnCrystalValueChange;

    public void InitPlayer()
    {

        Health = 100;

        Level = 1;
        xpForNextLvl = 20f;
        XP = 0f;


        Kills = 0;
        Bullets = 50;
        Rockets = 1;

        Crystal = 0;
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

    public int Level
    {
        get => lvl;
        set
        {
            lvl = value;
            OnLevelChange?.Invoke(lvl);
        }
    }

    public float XP
    {
        get => xp;
        set
        {
            xp = value;

            if (xp >= xpForNextLvl)
            {
                Level++;
                xp = 0f;
                xpForNextLvl *= 1.15f;
            }

            OnXPChange?.Invoke(xp);

        }
    }
    public float xpForNextLvl { get; set; }

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

    public int Crystal
    {
        get => crystal;
        set
        {
            crystal = value;
            OnCrystalValueChange?.Invoke(crystal);
        }
    }

    //auto aim Delay
    //Headshot - x2 dmg
    //Homing missiles 
    //Mine drops from corpse 


    //Rifle upgrades

    //Rocket Upgrades
    //Direct rockets
    //Homing missiles
    //Tactical nuclear rockets

    //Laser upgrades

    //Bots upgrade

}
