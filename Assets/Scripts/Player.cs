using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ActionController actionController;
    
   
    private Animator _animator;
    private CharacterController _characterController;
    [Header("Move Setup")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float turnSpeed = 0.1f;

    [Header("Shooting Setup")]
    [SerializeField] private ParticleSystem PsShooting;
    [SerializeField] private ParticleSystem PsShooting2;
    [SerializeField] private ParticleSystem PsShooting3;
    
    private float _turnSmoothVelocity;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }


    void FixedUpdate()
    {
        Move();

    }

    private void Shoot(bool isFire)
    {
        if (isFire)
        {
            PsShooting.Play();
            PsShooting2.Play();
            PsShooting3.Play();
        }
        else
        {
            PsShooting.Stop(true);
        }
    }
    private void Move()
    {
        if(!_characterController.isGrounded)
        {
            _characterController.Move(Vector3.down * (moveSpeed * Time.deltaTime));
        }
        
        var direction = new Vector3(actionController.Move.x, 0, actionController.Move.y);
        
        _animator.SetFloat("Move", direction.magnitude);
        
        if(direction.magnitude >= 0.01f)
        {
            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSpeed);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            
            if (direction.magnitude >= 0.25f)
            {
                _characterController.Move(direction * (moveSpeed * Time.deltaTime));
            }
        }
    }

    private void OnEnable()
    {
        actionController.OnFireBoolChange += Shoot;
    }
    
    private void OnDisable()
    {
        actionController.OnFireBoolChange -= Shoot;
    }
}


