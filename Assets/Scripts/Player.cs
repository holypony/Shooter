using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private ActionController actionController;
    
    [SerializeField] private GameSetupSo gameSetupSo;
    private Animator _animator;
    private CharacterController _characterController;
    
    [Header("Move Setup")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float turnSpeed = 0.1f;
    private float _turnSmoothVelocity;
    
    [Header("Shooting Setup")]
    [SerializeField] private ParticleSystem PsShooting;
    [SerializeField] private ParticleSystem PsShooting2;
    [SerializeField] private ParticleSystem PsShooting3;



    private bool isShooting = false;
 
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        Move();
        Aim();
    }
    private void Move()
    {
        if(!_characterController.isGrounded)
        {
            _characterController.Move(Vector3.down * (moveSpeed * Time.deltaTime));
        }
        
        var direction = new Vector3(actionController.Move.x, 0, actionController.Move.y);
        
        _animator.SetFloat("Move", direction.magnitude);
        
        if (direction.magnitude >= 0.1f)
        {
            _characterController.Move(direction * (moveSpeed * Time.deltaTime));
        }
    }

    private void Aim()
    {
        var direction = new Vector3(actionController.Aim.x, 0, actionController.Aim.y);
        if(direction.magnitude >= 0.01f)
        {
            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSpeed);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            if (actionController.IsAiming)
            {
                if (direction.magnitude >= 0.65f && gameSetupSo.Bullets > 0)
                {
                    
                    PsShooting.Play();
                    PsShooting2.Play();
                    PsShooting3.Play();

                    if (!isShooting)
                    {
                        StartCoroutine(Shooting());
                    }
                    IEnumerator Shooting()
                    {
                        isShooting = true;
                        while (isShooting)
                        {
                           
                            gameSetupSo.Bullets--;
                            SoundManager.instance.PlayRifleShot();
                            
                            if (gameSetupSo.Bullets < 1)
                            {
                                isShooting = false;
                            }
                            
                            yield return new WaitForSeconds(0.09f); 
                        }
                    }
                }
                else
                {
                    isShooting = false;
                    PsShooting.Stop(true);
                }
            }
        }
        else
        {
            isShooting = false;
            PsShooting.Stop(true);
        }
    }
}


