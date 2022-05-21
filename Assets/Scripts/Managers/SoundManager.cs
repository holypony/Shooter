using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;
    [SerializeField] private GameSetupSo gameSetupSo;
    [SerializeField] private AudioSource AsWeapon;
    [SerializeField] private AudioSource AsBg;
    [SerializeField] private AudioSource AsOrk;

    public void SoundsSwitcher()
    {
        if (!gameSetupSo.IsSound)
        {
            gameSetupSo.IsSound = true;
            
            AsWeapon.mute = true;
            AsBg.mute = true;
            AsOrk.mute = true;
        }
        else
        {
            gameSetupSo.IsSound = false;
            
            AsWeapon.mute = false;
            AsBg.mute = false;
            AsOrk.mute = false;
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

    public void OrkDeath()
    {
        AsOrk.Play();
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
