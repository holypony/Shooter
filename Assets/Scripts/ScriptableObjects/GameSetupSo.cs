using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/GameData")]
public class GameSetupSo : ScriptableObject
{
    [SerializeField] private bool isPlay;
    [SerializeField] private int timeLeft;
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
    
    public int TimeLeft
    {
        get => timeLeft;
        set
        {
            timeLeft = value;
            OnTimeLeftChange?.Invoke(timeLeft);
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
    public event Action<int> OnTimeLeftChange;
    public event Action<int> OnBulletsChange;
    public event Action<int> OnKillsChange;
    public event Action<Vector3> OnTargetChange;


}