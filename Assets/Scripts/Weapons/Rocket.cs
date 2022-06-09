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
    [SerializeField] private GameSetupSo gameSetupSo;

    private Collider collider;
    private bool targetHit = false;
    void Awake()
    {

        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        mr = GetComponent<MeshRenderer>();
    }

    void OnEnable()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        targetHit = false;

        audioSource.clip = soundLaunch;
        if (gameSetupSo.IsSound) audioSource.Play();
        collider.enabled = true;
        mr.enabled = true;
        StartCoroutine(live());
        IEnumerator live()
        {
            yield return new WaitForSeconds(6f);
            gameObject.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        if (targetHit) return;

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
        if (!targetHit)
        {
            if (col.transform.CompareTag("Enemy"))
            {
                Explode();
                targetHit = true;
            }
            else
            {
                Explode();
                targetHit = true;
            }
        }
    }

    private void Explode()
    {

        collider.enabled = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        mr.enabled = false;
        explode.Play();

        audioSource.clip = SoundExplode;
        if (gameSetupSo.IsSound) audioSource.Play();
        //gameObject.SetActive(false);
    }

    private void turnOff()
    {
        gameObject.SetActive(false);
    }
}
