using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
public class EnemySoldier : Bot
{
    private Animator _animator;
    private Rigidbody[] _rigidbodies;
    private Collider[] _colliders;

    [Header("Shooting Setup")]
    [SerializeField] private GameObject[] trophies;
    [SerializeField] private ParticleSystem PsBlood;
    private Vector3 BulletPos;
    [SerializeField] private Vactor3ListSO listPos;
    [SerializeField] private GameSetupSo gameSetupSo;
    private NavMeshAgent _agent;
    [SerializeField] private bool test = false;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
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

        if (test)
        {

            Death(1300f, Vector3.forward);
            test = false;
        }
    }


    public List<Vector3> pos;
    public void SaveLocalPos()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        foreach (var t in _rigidbodies)
        {
            pos.Add(t.transform.localPosition);
        }

        listPos.Pos = pos;
    }

    private void LoadLocalPos()
    {
        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _rigidbodies[i].transform.localPosition = listPos.Pos[i];
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

        float rndSnd = Random.Range(0f, 1f);
        if (rndSnd > 0.6f) SoundManager.instance.OrkDeath();

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
            foreach (var body in _rigidbodies)
            {
                body.isKinematic = false;
                body.AddForce(dir * force);
            }
        }
        else
        {
            LoadLocalPos();
            foreach (var body in _rigidbodies)
            {
                body.isKinematic = true;
            }
        }
    }
}
