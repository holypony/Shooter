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
    
    [SerializeField] private TMP_Text bulletsQuantityText;
    [SerializeField] private TMP_Text killsText;


    private void updateIsPlay(bool isPlay)
    {
        StartPanel.SetActive(!isPlay);
    }
    
    private void UpdateKills(int timeLeft)
    {
        killsText.text = "Kills: " + gameSetupSo.Kills;
    }
    
    private void UpdateBullets(int bullets)
    {
        bulletsQuantityText.text = "Bullets: " + bullets;

    }

    public void OpenSettingPanel(bool isOpen)
    {
        SettingPanel.SetActive(isOpen);
    }
    
    private void OnEnable()
    {
        bulletsQuantityText.text = "Bullets: " + gameSetupSo.Bullets;
        
        gameSetupSo.OnIsPlayChange += updateIsPlay;
        gameSetupSo.OnKillsChange += UpdateKills;
        gameSetupSo.OnBulletsChange += UpdateBullets;
    }

    private void OnDisable()
    {
        gameSetupSo.OnIsPlayChange -= updateIsPlay;
        gameSetupSo.OnKillsChange -= UpdateKills;
        gameSetupSo.OnBulletsChange -= UpdateBullets;
    }
}
