using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Other")]
    [SerializeField] private GameSetupSo gameSetupSo;
    [SerializeField] private SoliderPool soliderPool;
    [SerializeField] private UiManager uiManager;
    
    [Header("Game settings")]
    [SerializeField] private int matchTime = 45;

    private void Awake()
    {
        
        gameSetupSo.IsPlay = false;
    }

    private void MatchManager(bool isPlay)
    {
        
        
        if (isPlay)
        {
            gameSetupSo.Kills = 0;
            gameSetupSo.Bullets = 50;
            gameSetupSo.Health = 100;


        }
    }
    
    private void OnEnable()
    {
        gameSetupSo.OnIsPlayChange += MatchManager;
    }

    private void OnDisable()
    {
        gameSetupSo.OnIsPlayChange -= MatchManager;
    }
}
