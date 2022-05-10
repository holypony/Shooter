using System;
using UnityEngine;

public class Mine : MonoBehaviour
{
    
    [SerializeField] private ParticleSystem explosionPs;
    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            explosionPs.Play(true);
            _audioSource.Play();
        }
    }
}
