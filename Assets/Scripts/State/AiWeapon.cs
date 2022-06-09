using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiWeapon : MonoBehaviour
{
    [Header("Shooting Setup")]
    [SerializeField] private ParticleSystem PsShooting;
    [SerializeField] private ParticleSystem PsShooting2;
    [SerializeField] private ParticleSystem PsShooting3;

    [Header("Rocket launcher")]
    [SerializeField] private GameObject rocketPrefab;
    [SerializeField] private GameObject firePoint;
    private bool isShooting = false;


    public void Shoot()
    {

        if (!isShooting) StartCoroutine(Shot());

        IEnumerator Shot()
        {
            isShooting = true;

            yield return new WaitForSeconds(0.5f);
            PsShooting.Play();
            PsShooting2.Play();
            PsShooting3.Play();
            SoundManager.instance.PlayRifleShot();
            isShooting = false;
        }

    }
}
