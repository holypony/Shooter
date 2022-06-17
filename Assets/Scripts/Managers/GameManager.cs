using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Other")]
    [SerializeField] private GameSetupSo gameSetupSo;
    [SerializeField] private PlayerSO playerSO;
    [SerializeField] private SoliderPool soliderPool;
    [SerializeField] private UiManager uiManager;

    [Header("Game settings")]
    [SerializeField] private int matchTime = 45;

    private void Awake()
    {
        gameSetupSo.IsPlay = false;
        gameSetupSo.IsPause = false;
    }

    private void MatchManager(bool isPlay)
    {
        if (isPlay) playerSO.InitPlayer();
    }

    private void Pause(int lvl)
    {
        //if (lvl == 1) return;
        // gameSetupSo.IsPause = true;
    }

    private void OnEnable()
    {
        //playerSO.OnLevelChange += Pause;
        gameSetupSo.OnIsPlayChange += MatchManager;
    }

    private void OnDisable()
    {
        //playerSO.OnLevelChange += Pause;
        gameSetupSo.OnIsPlayChange -= MatchManager;
    }
}
