using System;
using UnityEngine;

public class EnemySoldier : Bot
{
    private Animator _animator;
    private Rigidbody _rb;
    private CapsuleCollider _collider;
    private Rigidbody[] _rbs;
    private Collider[] _colliders;
    
    [SerializeField] private ParticleSystem PsBlood;
    [SerializeField] private ParticleSystem PsBlood2;
    [SerializeField] private ParticleSystem PsBlood3;

    public bool test = false;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _rbs = GetComponentsInChildren<Rigidbody>();

        _collider = GetComponent<CapsuleCollider>();
        _colliders = GetComponentsInChildren<Collider>();

    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Bullet"))
        {
            Death();
        }
    }

    private void Update()
    {
        if (test)
        {
            test = false;
            Death();
        }
    }

    private void OnEnable()
    {
        Init();
    }

    private void OnDisable()
    {
        IsAlive = false;
    }

    void Init()
    {
        IsAlive = true;
        _animator.enabled = true;
        ColliderControl(true);
        RigidBodyControl(true);
    }

    private void Death()
    {

        PsBlood.Play(true);
        //PsBlood2.Play();
        //PsBlood3.Play();
        _animator.enabled = false;
        ColliderControl(false);
        RigidBodyControl(false);

    }

    private void ColliderControl(bool state)
    {
        
        foreach (var coll in _colliders)
        {
            coll.enabled = !state;
        }
        _collider.enabled = state;
    }
    
    private void RigidBodyControl(bool state)
    {
        
        foreach (var body in _rbs)
        {
            body.isKinematic = state;
        }
        _rb.isKinematic = !state;
    }
}
