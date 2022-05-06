using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TextCore;

public class EnemySoldier : Bot
{
    private Animator _animator;
    private Rigidbody _rb;
    private CapsuleCollider _collider;
    private Rigidbody[] _rbs;
    private Collider[] _colliders;
    
    [Header("Shooting Setup")]
    [SerializeField] private ParticleSystem PsShooting;
    [SerializeField] private ParticleSystem PsShooting2;
    [SerializeField] private ParticleSystem PsShooting3;
    
    [SerializeField] private ParticleSystem PsBlood;
    
    private Vector3 BulletPos;

    
    public bool test = false;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _rbs = GetComponentsInChildren<Rigidbody>();

        _collider = GetComponent<CapsuleCollider>();
        _colliders = GetComponentsInChildren<Collider>();

        _agent = GetComponent<NavMeshAgent>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if (!other.CompareTag("Bullet")) return;
        BulletPos = other.transform.position;
        Death();
    }

    private void Update()
    {
        if (test)
        {
            test = false;
            Death();
        }
    }


    public override void Init()
    {
        base.Init();
        IsAlive = true;
        _animator.enabled = true;

        ColliderControl(true);
        RigidBodyControl(true);

        _agent.SetDestination(target.transform.position);
    }

    private void FixedUpdate()
    {
        float dist = Vector3.Distance(transform.position, player.transform.position);
        if (dist < 5 && IsAlive)
        {
            _agent.isStopped = true;
            transform.LookAt(player.transform);
            Shoot(true);
        }
        else
        {
            Shoot(false);
        }

        _animator.SetFloat("Move", _agent.remainingDistance > 0 ? 1f : 0f);
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

    private void Death()
    {
        
        IsAlive = false;
        
        _agent.isStopped = true;
        
        _animator.enabled = false;
        
        PsBlood.Play(true);
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
        var dir = transform.position - BulletPos;

 
        // force = 1 / dist 
        foreach (var body in _rbs)
        {
            body.isKinematic = state;
            
                body.AddForce(dir * 750f);
        }
        _rb.isKinematic = !state;
    }
}
