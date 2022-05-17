using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    [SerializeField] private AudioSource AsWeapon;



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

   
    
    private void OnEnable()
    {
        //actionController.OnFireBoolChange += PlayRifleShot;
    }
    
    private void OnDisable()
    {
        //actionController.OnFireBoolChange -= PlayRifleShot;
    }
}
