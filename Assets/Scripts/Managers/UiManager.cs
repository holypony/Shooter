using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameSetupSo gameSetupSo;

    [Header("UI Panels")]
    [SerializeField] private GameObject SettingPanel;
    [SerializeField] private GameObject StartPanel;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject InGamePanel;

    [Header("Texts")]
    [SerializeField] private TMP_Text bulletsQuantityText;
    [SerializeField] private TMP_Text rocketsQuantityText;
    [SerializeField] private TMP_Text killsText;
    [SerializeField] private TMP_Text healthText;

    [Header("Start Button")]
    [SerializeField] private GameObject StartBtn;

    [Header("Sprites")]
    [SerializeField] private Sprite soundOff;
    [SerializeField] private Sprite soundOn;
    [Header("Sounds Buttons")]
    [SerializeField] private Button SoundSwitcherStart;
    [SerializeField] private Button SoundSwitcherSetting;
    [SerializeField] private Button SoundSwitcherGameOver;

    private void Start()
    {
        StartBtn.transform.DOScale(1.2f, 1f).SetLoops(-1, LoopType.Yoyo);

    }



    private void updateIsPlay(bool isPlay)
    {
        if (!isPlay)
        {
            StartPanel.SetActive(false);
            GameOverPanel.SetActive(true);
        }
        else
        {
            InGamePanel.SetActive(true);
            GameOverPanel.SetActive(false);
        }
    }

    private void UpdateKills(int timeLeft)
    {
        killsText.text = "Kills: " + gameSetupSo.Kills;
    }

    private void UpdateBullets(int bullets)
    {
        bulletsQuantityText.text = "Bullets: " + bullets;
    }
    private void UpdateRockets(int rockets)
    {
        rocketsQuantityText.text = "Rockets: " + rockets;
    }

    private void UpdateHealth(int obj)
    {
        if (obj > 20)
        {
            healthText.color = Color.white;
        }
        else
        {
            healthText.color = Color.red;
        }
        healthText.text = "Hp: " + obj;
    }

    public void OpenSettingPanel(bool isOpen)
    {
        SettingPanel.SetActive(isOpen);
    }

    private void ChangeSoundsBtnImgs(bool isSound)
    {
        if (isSound)
        {
            SoundSwitcherStart.image.sprite = soundOn;
            SoundSwitcherGameOver.image.sprite = soundOn;
        }
        else
        {
            SoundSwitcherStart.image.sprite = soundOff;
            SoundSwitcherGameOver.image.sprite = soundOff;
        }

    }

    private void OnEnable()
    {
        bulletsQuantityText.text = "Bullets: " + gameSetupSo.Bullets;

        gameSetupSo.OnIsPlayChange += updateIsPlay;
        gameSetupSo.OnIsSoundChange += ChangeSoundsBtnImgs;
        gameSetupSo.OnKillsChange += UpdateKills;
        gameSetupSo.OnBulletsChange += UpdateBullets;
        gameSetupSo.OnRocketsChange += UpdateRockets;
        gameSetupSo.OnHealthChange += UpdateHealth;
    }

    private void OnDisable()
    {
        gameSetupSo.OnIsPlayChange -= updateIsPlay;
        gameSetupSo.OnIsSoundChange -= ChangeSoundsBtnImgs;
        gameSetupSo.OnKillsChange -= UpdateKills;
        gameSetupSo.OnBulletsChange -= UpdateBullets;
        gameSetupSo.OnRocketsChange -= UpdateRockets;
        gameSetupSo.OnHealthChange -= UpdateHealth;
    }
}
