using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Vehicles : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private ParticleSystem explode;
    [SerializeField] private GameSetupSo gameSetupSo;
    [SerializeField] private GameObject go;

    [SerializeField] private AudioClip soundLaunch;
    [SerializeField] private AudioClip SoundExplode;
    [SerializeField] private AudioSource audioSource;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Rocket"))
        {
            Explode();
        }

        if (collision.transform.CompareTag("Vehicle"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        explode.Play();
        go.SetActive(false);
        if (gameSetupSo.IsSound) audioSource.Play();

    }

    private void OnEnable()
    {
        go.SetActive(true);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        StartCoroutine(live());
        IEnumerator live()
        {
            yield return new WaitForSeconds(6f);
            gameObject.SetActive(false);
        }

    }

    private void OnDisable()
    {
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
    }

    private void FixedUpdate()
    {

        var locVel = transform.InverseTransformDirection(rb.velocity);
        locVel.z = speed;
        rb.velocity = transform.TransformDirection(locVel);
    }

}
