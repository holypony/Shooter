using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    [SerializeField] private AudioSource AsWeapon;

    [SerializeField] private AudioClip rifleShot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayRifleShot()
    {
        AsWeapon.clip = rifleShot; 
        AsWeapon.Play();
    }
}
