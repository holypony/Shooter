using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/GameData")]
public class GameSetupSo : ScriptableObject
{
    [SerializeField] private bool isPlay;
    [SerializeField] private bool isSound;
    [SerializeField] private bool isPause;
    [SerializeField] private int difficultyLvl;



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
    public bool IsPause
    {
        get => isPause;
        set
        {
            isPause = value;
            OnIsPause?.Invoke(isPause);
        }
    }

    public int DifficultyLvl
    {
        get => difficultyLvl;
        set
        {
            difficultyLvl = value;
        }
    }



    public event Action<bool> OnIsPlayChange;
    public event Action<bool> OnIsPause;
    public event Action<bool> OnIsSoundChange;

}