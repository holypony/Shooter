using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Vehicles : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private ParticleSystem explode;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Rocket"))
        {
            explode.Play();
        }

        if (collision.transform.CompareTag("Vehicle"))
        {
            explode.Play();
        }
    }

    private void OnEnable()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        StartCoroutine(move());

        IEnumerator move()
        {
            //var trPos = transform.position;
            //var trRot = transform.rotation;

            yield return new WaitForSeconds(5f);
            gameObject.SetActive(false);

            //rb.velocity = Vector3.zero;

            //var transform1 = transform;
            //transform1.position = trPos;
            //transform1.rotation = trRot;
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

        //transform.position += transform.forward * (speed * Time.deltaTime);
    }
}
