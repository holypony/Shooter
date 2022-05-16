using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicles : MonoBehaviour
{
  private Rigidbody rb;
  [SerializeField] private float speed = 5f;
  
  private void Awake()
  {
    rb = GetComponent<Rigidbody>();

    StartCoroutine(move());
    IEnumerator move()
    {
      var trPos = transform.position;
      var trRot = transform.rotation;
      while (true)
      {
        yield return new WaitForSeconds(5f);
        
        rb.velocity = Vector3.zero;
        
        var transform1 = transform;
        transform1.position = trPos;
        transform1.rotation = trRot;

      }
      
    }
  }

  private void FixedUpdate()
  {
    
    var locVel = transform.InverseTransformDirection(rb.velocity);
    locVel.z = speed;
    rb.velocity = transform.TransformDirection(locVel);
    
  }
}
