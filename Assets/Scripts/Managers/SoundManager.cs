using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;
    [SerializeField] private GameSetupSo gameSetupSo;
    [SerializeField] private AudioSource AsWeapon;
    [SerializeField] private AudioSource AsBg;
    
    [SerializeField] private AudioSource AsOrk1;
    [SerializeField] private AudioSource AsOrk2;

    [SerializeField] private AudioClip[] deathRattles;
    [SerializeField] private AudioSource[] orkAs;

    public void SoundsSwitcher()
    {
        if (!gameSetupSo.IsSound)
        {
            gameSetupSo.IsSound = true;
            
            AsWeapon.mute = true;
            AsBg.mute = true;
            AsOrk1.mute = true;
            AsOrk2.mute = true;

        }
        else
        {
            gameSetupSo.IsSound = false;
            
            AsWeapon.mute = false;
            AsBg.mute = false;
            AsOrk1.mute = false;
            AsOrk2.mute = false;
        }
    }
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }
        Destroy(this.gameObject);
    }
    
    public void PlayRifleShot()
    {
        AsWeapon.Play();
    }

    private int i = 0;
    
    public void OrkDeath()
    {
        orkAs[i].clip = deathRattles[Random.Range(0, 3)];
        orkAs[i].Play();
        i++;
        
        if (i == 1) i = 0;
    }
    
   
    
    private void OnEnable()
    {
        //actionController.OnFireBoolChange += PlayRifleShot;
    }
    
    private void OnDisable()
    {
        //actionController.OnFireBoolChange -= PlayRifleShot;
    }
}
