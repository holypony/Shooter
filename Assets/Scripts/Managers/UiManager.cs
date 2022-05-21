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
    [SerializeField] private GameObject GameOverPanel;
    
    [SerializeField] private TMP_Text bulletsQuantityText;
    [SerializeField] private TMP_Text killsText;
    [SerializeField] private TMP_Text healthText;


    private void updateIsPlay(bool isPlay)
    {
        if (!isPlay)
        {
            StartPanel.SetActive(false);
            GameOverPanel.SetActive(true);
        }
        else
        {
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
    
    
    private void UpdateHealth(int obj)
    {
        if (obj > 20)
        {
            healthText.color =Color.white;
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
    
    private void OnEnable()
    {
        bulletsQuantityText.text = "Bullets: " + gameSetupSo.Bullets;
        
        gameSetupSo.OnIsPlayChange += updateIsPlay;
        gameSetupSo.OnKillsChange += UpdateKills;
        gameSetupSo.OnBulletsChange += UpdateBullets;
        gameSetupSo.OnHealthChange += UpdateHealth;
    }

    private void OnDisable()
    {
        gameSetupSo.OnIsPlayChange -= updateIsPlay;
        gameSetupSo.OnKillsChange -= UpdateKills;
        gameSetupSo.OnBulletsChange -= UpdateBullets;
        gameSetupSo.OnHealthChange -= UpdateHealth;
    }
}
