using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/GameData")]
public class GameSetupSo : ScriptableObject
{
    [SerializeField] private bool isPlay;

    public bool IsPlay
    {
        get => isPlay;
        set
        {
            isPlay = value;
            OnIsPlayChange?.Invoke(isPlay);
        }

    }
    public event Action<bool> OnIsPlayChange;

}