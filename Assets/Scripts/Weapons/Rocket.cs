using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private ParticleSystem explode;
    [SerializeField] private float speed = 35f;
    private MeshRenderer mr;
    private Rigidbody rb;

    [SerializeField] private AudioClip soundLaunch;
    [SerializeField] private AudioClip SoundExplode;
    [SerializeField] private AudioSource audioSource;


    private bool targetHit = false;
    void Awake()
    {

        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
    }

    void OnEnable()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        targetHit = false;

        audioSource.clip = soundLaunch;
        audioSource.Play();

        mr.enabled = true;
        Invoke("turnOff", 6f);
    }
    private void FixedUpdate()
    {
        if (targetHit)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            return;
        }
        var locVel = transform.InverseTransformDirection(rb.velocity);
        locVel.z = speed;
        rb.velocity = transform.TransformDirection(locVel);

    }

    private void Update()
    {
        //if (targetHit) return;
        //transform.position += transform.forward * (speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (targetHit) return;
        if (col.transform.CompareTag("Enemy"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        targetHit = true;
        mr.enabled = false;
        explode.Play();

        audioSource.clip = SoundExplode;
        audioSource.Play();
        //gameObject.SetActive(false);
    }

    private void turnOff()
    {
        gameObject.SetActive(false);
    }
}
