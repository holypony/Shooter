using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [Header("Other")]
    [SerializeField] private GameSetupSo gameSetupSo;
    [SerializeField] private SoliderPool soliderPool;
    [SerializeField] private UiManager uiManager;

    [Header("Game settings")]
    [SerializeField] private int matchTime = 45;

    [SerializeField] private CinemachineVirtualCamera playCam;
    [SerializeField] private CinemachineVirtualCamera menuCam;

    private void Awake()
    {
        gameSetupSo.IsPlay = false;
    }

    private void SwitchCameras()
    {
        playCam.gameObject.SetActive(true);
        menuCam.gameObject.SetActive(false);

    }

    private void MatchManager(bool isPlay)
    {
        if (isPlay)
        {
            gameSetupSo.Kills = 0;
            gameSetupSo.Bullets = 50;
            gameSetupSo.Rockets = 1;
            gameSetupSo.Health = 100;
            SwitchCameras();
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
