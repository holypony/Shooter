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
    [SerializeField] private GameObject root;
    
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

        _animator.enabled = true;

        ColliderControl(true);
        RigidBodyControl(true);

        _agent.SetDestination(target.transform.position);
    }

    private float dist;
    private bool isShooting = false;
 
    private void FixedUpdate()
    {
        if (IsAlive)
        {
            dist = Vector3.Distance(transform.position, player.transform.position);
            if (dist < 5 && IsAlive)
            {
                transform.LookAt(player.transform);
                if (!isShooting)
                {
                    isShooting = true;
                    _agent.isStopped = true;
                    Shoot(true); 
                }
            
            }
            else
            {
                isShooting = false;
                _agent.isStopped = false;
                Shoot(false);
            }

            _animator.SetFloat("Move", _agent.remainingDistance > 0.3f ? 1f : 0f);
        }
        
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
        Shoot(false);
        
        IsAlive = false;
        
        _agent.isStopped = true;
        
        _animator.enabled = false;
        
        PsBlood.Play(true);
        ColliderControl(false);
        RigidBodyControl(false);

        //StartCoroutine(BackToPool());
        IEnumerator BackToPool()
        {
            yield return new WaitForSeconds(7f);
            gameObject.SetActive(false);

        }
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
        if (state == false && dist > 0)
        {
            var dir = transform.position - BulletPos;
            var distIndex = 750f * (1f / dist);
            
            foreach (var body in _rbs)
            {
                body.isKinematic = false;
                body.AddForce(dir * distIndex);
            }
            _rb.isKinematic = true;
        }
        else
        {
            foreach (var body in _rbs)
            {
                body.isKinematic = true;
            }
            _rb.isKinematic = false;
        }
        
        
       
        
        // force = 1 / dist 
        
    }
}
