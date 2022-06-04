using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
public class EnemySoldier : Bot
{
    private Animator _animator;
    //private Rigidbody _rb;
    //private CapsuleCollider _collider;
    private Rigidbody[] _rbs;
    private Collider[] _colliders;

    [Header("Shooting Setup")]
    [SerializeField] private GameObject[] trophies;
    [SerializeField] private ParticleSystem PsBlood;
    private Vector3 BulletPos;
    [SerializeField] private Vactor3ListSO listPos;
    [SerializeField] private GameSetupSo gameSetupSo;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        //_rb = GetComponent<Rigidbody>();
        _rbs = GetComponentsInChildren<Rigidbody>();
        //_collider = GetComponent<CapsuleCollider>();
        _colliders = GetComponentsInChildren<Collider>();
        _agent = GetComponent<NavMeshAgent>();
    }

    private float _dist;

    private float cooldown = 1f;
    private float lastHit = 0.9f;


    private void FixedUpdate()
    {
        _dist = Vector3.Distance(transform.position, PlayerTarget.transform.position);
        if (_dist < 1f && IsAlive)
        {
            if (lastHit > cooldown)
            {
                lastHit = 0;

                gameSetupSo.Health -= 20;
                if (gameSetupSo.Health < 1)
                {
                    gameSetupSo.IsPlay = false;
                }
            }
            else
            {
                lastHit += Time.deltaTime;
            }
        }
    }
    /*
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Bullet"))
        {
            BulletPos = other.transform.position;
            Death(150f);
        }

        if (other.CompareTag("Mine"))
        {
            BulletPos = other.transform.position;
            Death(600f);
        }

        if (other.CompareTag("Rocket"))
        {
            BulletPos = other.transform.position;
            Death(800f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Mine"))
        {
            BulletPos = collision.transform.position.normalized;
            Death(600f);
        }

        if (collision.transform.CompareTag("Vehicle"))
        {
            BulletPos = collision.transform.position.normalized;
            Death(600f);
        }
    }
    */

    public List<Vector3> pos;
    // ReSharper disable Unity.PerformanceAnalysis
    public void SaveLocalPos()
    {
        _rbs = GetComponentsInChildren<Rigidbody>();
        foreach (var t in _rbs)
        {
            pos.Add(t.transform.localPosition);
        }

        listPos.Pos = pos;
    }

    private void LoadLocalPos()
    {
        for (int i = 0; i < _rbs.Length; i++)
        {
            _rbs[i].transform.localPosition = listPos.Pos[i];
        }
    }

    public override void Init()
    {
        IsAlive = true;

        trophies[Random.Range(0, 2)].SetActive(true);

        ColliderControl(true);
        RigidBodyControl(true, Vector3.zero);

        _animator.enabled = true;
        _animator.SetFloat("Move", 1f);

        StartCoroutine(UpdateTarget());
        IEnumerator UpdateTarget()
        {
            while (IsAlive)
            {
                _agent.SetDestination(PlayerTarget.transform.position);
                yield return new WaitForSeconds(1f);
            }
        }
    }

    public void Death(float force, Vector3 bulletPos)
    {
        RigidBodyControl(false, bulletPos, force);
        ColliderControl(false);

        SoundManager.instance.OrkDeath();

        foreach (var trophy in trophies)
        {
            trophy.SetActive(false);
        }

        IsAlive = false;

        _agent.isStopped = true;

        _animator.enabled = false;

        PsBlood.Play(true);



        StartCoroutine(BackToPool());
        IEnumerator BackToPool()
        {
            yield return new WaitForSeconds(1f);
            gameSetupSo.Kills++;
            SpawnBox();
            gameObject.SetActive(false);
        }
    }

    private void SpawnBox()
    {
        int i = Random.Range(0, 3);
        if (i < 2) return;
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z);
        bonusManager.SpawnBox(pos);
    }

    private void ColliderControl(bool state)
    {
        foreach (var coll in _colliders)
        {
            coll.enabled = !state;
        }
        _colliders[0].enabled = state;
    }

    private void RigidBodyControl(bool state, Vector3 bulletPos, float force = 0)
    {

        if (!state)
        {
            var dir = transform.position - bulletPos;
            foreach (var body in _rbs)
            {
                body.isKinematic = false;
                body.AddForce(dir * force);
            }
        }
        else
        {
            LoadLocalPos();
            foreach (var body in _rbs)
            {
                body.isKinematic = true;
            }
        }
    }
}
