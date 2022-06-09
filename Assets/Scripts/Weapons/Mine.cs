using System;
using UnityEngine;

public class Mine : MonoBehaviour
{

    [SerializeField] private ParticleSystem explosionPs;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private CameraShaker _cameraShaker;
    [SerializeField] private GameSetupSo gameSetupSo;
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //_cameraShaker.CameraShake();

            if (gameSetupSo.IsSound) _audioSource.Play();
            explosionPs.Play(true);

            CameraShaker.instance.CameraShake();
            Destroy(gameObject, 3f);
        }
    }
}
