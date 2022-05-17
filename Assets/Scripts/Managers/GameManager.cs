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
        gameSetupSo.Kills = 0;
        gameSetupSo.Bullets = 3;
        gameSetupSo.IsPlay = false;
    }

    private void MatchManager(bool isPlay)
    {
        if (isPlay)
        {
            gameSetupSo.TimeLeft = matchTime;
            StartCoroutine(Timer());
            IEnumerator Timer()
            {
                while (1 < gameSetupSo.TimeLeft)
                {

                    gameSetupSo.TimeLeft--;
                    yield return new WaitForSeconds(1f);
                }

                gameSetupSo.IsPlay = false;
            }  
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
