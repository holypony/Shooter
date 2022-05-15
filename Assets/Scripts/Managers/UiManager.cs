using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;


public class UiManager : MonoBehaviour
{
    [SerializeField] private GameSetupSo gameSetupSo;
    [SerializeField] private GameObject SettingPanel;
    
    [SerializeField] private TMP_Text healthText;
    // Start is called before the first frame update
    void Start()
    {
        //gameSetupSo.OnIsPlayChange += GameOver;
        gameSetupSo.OnPlayerHealthChange += updateHealth;
    }

    private void updateHealth(float health)
    {
        healthText.text = "Hp: " + health;
    }

    private void GameOver(bool obj)
    {
        //throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenSettingPanel(bool isOpen)
    {
        SettingPanel.SetActive(isOpen);
    }
}
