using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;
    [SerializeField] private ActionController actionController;
    [SerializeField] private AudioSource AsWeapon;

    [SerializeField] private AudioClip rifleShot;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }
        Destroy(this.gameObject);
    }
    
    private void PlayRifleShot(bool isFire)
    {
        if (isFire)
        {
            if (!isShooting)
            {
                StartCoroutine(SoundShooting());  
            }
        }
        else
        {
            isShooting = false;
        }
    }

    private bool isShooting = false;
    
    IEnumerator SoundShooting()
    {
        isShooting = true;
        while (isShooting)
        {
            AsWeapon.clip = rifleShot; 
            AsWeapon.Play();
            yield return new WaitForSeconds(0.06f); 
        }
    }
    
    private void OnEnable()
    {
        actionController.OnFireBoolChange += PlayRifleShot;
    }
    
    private void OnDisable()
    {
        actionController.OnFireBoolChange -= PlayRifleShot;
    }
}
