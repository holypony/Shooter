using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;


public class UiManager : MonoBehaviour
{
    [SerializeField] private GameSetupSo gameSetupSo;
    [SerializeField] private GameObject SettingPanel;
    [SerializeField] private GameObject StartPanel;

    
    [SerializeField] private TMP_Text timeLeftText;
    [SerializeField] private TMP_Text bulletsQuantityText;
    [SerializeField] private TMP_Text killsText;


    private void updateIsPlay(bool isPlay)
    {
        StartPanel.SetActive(!isPlay);
    }
    
    private void UpdateTime(int timeLeft)
    {
        timeLeftText.text = "Time left: " + timeLeft + "s";
        killsText.text = "Kills: " + gameSetupSo.Kills;
    }

    private void GameOver(bool obj)
    {

    }


    public void OpenSettingPanel(bool isOpen)
    {
        SettingPanel.SetActive(isOpen);
    }
    
    private void OnEnable()
    {
        gameSetupSo.OnIsPlayChange += updateIsPlay;
        gameSetupSo.OnTimeLeftChange += UpdateTime;
    }

    private void OnDisable()
    {
        gameSetupSo.OnIsPlayChange -= updateIsPlay;
        gameSetupSo.OnTimeLeftChange -= UpdateTime;
    }
}
