using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] private ParticleSystem explode;
    [SerializeField] private float speed = 35f;
    private MeshRenderer mr;
    private Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
    }

    void OnEnable()
    {
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        mr.enabled = true;
        Invoke("turnOff", 6f);
    }

    private void Update()
    {
        transform.position += transform.forward * (speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.transform.CompareTag("Enemy"))
        {
            Explode();
        }
    }

    private void Explode()
    {
        mr.enabled = false;
        explode.Play();
        Invoke("turnOff", 3f);
        //gameObject.SetActive(false);
    }

    private void turnOff()
    {
        gameObject.SetActive(false);
    }
}
