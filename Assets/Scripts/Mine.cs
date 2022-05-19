using System;
using UnityEngine;

public class Mine : MonoBehaviour
{
    
    [SerializeField] private ParticleSystem explosionPs;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private CameraShaker _cameraShaker;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //_cameraShaker.CameraShake();
            
            explosionPs.Play(true);
            _audioSource.Play();
            Destroy(gameObject, 3f);
        }
    }
}
