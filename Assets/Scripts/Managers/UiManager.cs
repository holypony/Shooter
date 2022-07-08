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
    [SerializeField] private PlayerSO playerSO;

    [Header("UI Panels")]
    [SerializeField] private GameObject SettingPanel;
    [SerializeField] private GameObject StartPanel;
    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject InGamePanel;
    [SerializeField] private GameObject LvlUpPanel;
    [SerializeField] private Slider ExpBar;

    [Header("Texts")]
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
        ChangeSoundsBtnImgs(gameSetupSo.IsSound);
    }

    private void updateIsPlay(bool isPlay)
    {
        if (!isPlay)
        {
            StartCoroutine(OneSecWait());
            IEnumerator OneSecWait()
            {
                yield return new WaitForSecondsRealtime(1f);
                StartPanel.SetActive(false);
                GameOverPanel.SetActive(true);
            }

        }
        else
        {
            InGamePanel.SetActive(true);
            GameOverPanel.SetActive(false);
        }
    }

    private void UpdateKills(int kills)
    {
        killsText.text = "Kills: " + kills;
        ExpBar.maxValue = playerSO.xpForNextLvl;
        ExpBar.value = playerSO.XP;
    }

    private void UpdateHealth(int hp)
    {
        if (hp > 20)
        {
            healthText.color = Color.white;
        }
        else
        {
            healthText.color = Color.red;
        }
        healthText.text = "Hp: " + hp;
    }

    private void LvlUp(int Lvl)
    {
        //LvlUpPanel.SetActive(true);
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
            SoundSwitcherSetting.image.sprite = soundOn;
            SoundSwitcherGameOver.image.sprite = soundOn;
        }
        else
        {
            SoundSwitcherStart.image.sprite = soundOff;
            SoundSwitcherSetting.image.sprite = soundOff;
            SoundSwitcherGameOver.image.sprite = soundOff;
        }
    }

    private void OnEnable()
    {

        gameSetupSo.OnIsPlayChange += updateIsPlay;
        gameSetupSo.OnIsSoundChange += ChangeSoundsBtnImgs;
        playerSO.OnKillsChange += UpdateKills;
        playerSO.OnLevelChange += LvlUp;
        playerSO.OnHealthChange += UpdateHealth;
    }

    private void OnDisable()
    {
        gameSetupSo.OnIsPlayChange -= updateIsPlay;
        gameSetupSo.OnIsSoundChange -= ChangeSoundsBtnImgs;
        playerSO.OnKillsChange -= UpdateKills;
        playerSO.OnLevelChange -= LvlUp;
        playerSO.OnHealthChange -= UpdateHealth;
    }



    public void PravacyPolice()
    {
        Application.OpenURL("https://telegra.ph/Privacy-Policy-06-02-14");
    }
}
