using System;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private Vactor3ListSO listpos;
    [SerializeField] private float agroDistance = 3f;
    public bool test = false;
    private NavMeshAgent _agent;
    
    private float dist;
    private bool isShooting = false;

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
        if (other.CompareTag("Bullet"))
        {
            BulletPos = other.transform.position;
            Death(50f);
        }
    
        if (other.CompareTag("Mine"))
        {
            BulletPos = other.transform.position;
            Death(600f);
        }
    }

    private void Update()
    {
        if (test)
        {
            test = false;
            Death(50f);
        }
    }

    public List<Vector3> pos;

    private void SaveLocalPos()
    {
        foreach (var t in _rbs)
        {
            pos.Add(t.transform.localPosition);
        }

        listpos.Pos = pos;
    }
    
    private void LoadLocalPos()
    {
        for (int i = 0; i < _rbs.Length; i++)
        {
            _rbs[i].transform.localPosition = listpos.Pos[i];
        }
    }
    
    public override void Init()
    {
        base.Init();

        ColliderControl(true);
        RigidBodyControl(true);
        _animator.enabled = true;
        _agent.SetDestination(target.transform.position);
    }
    
    private void FixedUpdate()
    {
        if (IsAlive)
        {
            dist = Vector3.Distance(transform.position, player.transform.position);
            if (dist < agroDistance && IsAlive)
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

    private void Death(float force)
    {
        Shoot(false);
        
        IsAlive = false;
        
        _agent.isStopped = true;
        
        _animator.enabled = false;
        
        PsBlood.Play(true);
        ColliderControl(false);
        RigidBodyControl(false, force);

        StartCoroutine(BackToPool());
        IEnumerator BackToPool()
        {
            yield return new WaitForSeconds(3f);
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

    private void RigidBodyControl(bool state, float force = 0)
    {

        if (!state)
        {
            var dir = transform.position - BulletPos;
            foreach (var body in _rbs)
            {
                body.isKinematic = false;
                body.AddForce(dir * force);
            }

            _rb.isKinematic = true;
        }
        else
        {
            LoadLocalPos();
            foreach (var body in _rbs)
            {
                body.isKinematic = true;
            }

            _rb.isKinematic = false;
        }
    }
}
