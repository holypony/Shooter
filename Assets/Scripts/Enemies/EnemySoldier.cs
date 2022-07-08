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
    [SerializeField] private ParticleSystem PsBlood;
    [SerializeField] private ParticleSystem PsSlash;
    [SerializeField] private Vactor3ListSO listPos;
    [SerializeField] private GameSetupSo gameSetupSo;
    [SerializeField] private PlayerSO playerSO;
    private NavMeshAgent _agent;

    private float _dist;
    public List<Vector3> pos;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        _colliders = GetComponentsInChildren<Collider>();
        _agent = GetComponent<NavMeshAgent>();
        //SaveLocalPos();
    }

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

        _agent.speed = 4f + gameSetupSo.DifficultyLvl;
        _agent.acceleration = 4f * (gameSetupSo.DifficultyLvl * 5f);
        _agent.angularSpeed = 12f * (gameSetupSo.DifficultyLvl * 5f);

        ColliderControl(true);
        RigidBodyControl(true, Vector3.zero);

        _animator.enabled = true;
        _animator.SetFloat("RUN", 1f);

        StartCoroutine(UpdateTarget());

        IEnumerator UpdateTarget()
        {
            _agent.SetDestination(PlayerTarget.transform.position);
            while (IsAlive)
            {

                while (gameSetupSo.IsPause)
                {
                    _agent.isStopped = true;
                    yield return new WaitForSeconds(1f);
                    if (!gameSetupSo.IsPause) _agent.isStopped = false;
                }

                _dist = Vector3.Distance(transform.position, PlayerTarget.transform.position);
                if (_dist > 1.5f)
                {

                    _agent.SetDestination(PlayerTarget.transform.position);
                    if (_dist > 35f) Death(0f, Vector3.zero);
                }
                else
                {
                    PsSlash.Play();
                    _animator.SetTrigger("ATTACK");

                    _agent.SetDestination(PlayerTarget.transform.position);
                    playerSO.Health -= 20;
                    if (playerSO.Health < 1)
                    {
                        gameSetupSo.IsPlay = false;
                    }
                    yield return new WaitForSeconds(0.5f);
                }
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    public void Death(float force, Vector3 bulletPos, bool isBlood = true)
    {
        RigidBodyControl(false, bulletPos, force);
        ColliderControl(false);

        float rndSnd = Random.Range(0f, 1f);
        if (rndSnd > 0.6f) SoundManager.instance.OrkDeath();

        IsAlive = false;

        _agent.isStopped = true;

        _animator.enabled = false;

        if (isBlood) PsBlood.Play(true);

        StartCoroutine(BackToPool());
        IEnumerator BackToPool()
        {
            playerSO.XP++;
            playerSO.Kills++;
            yield return new WaitForSeconds(2.5f);

            //SpawnBox();
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

    void OnDisable()
    {
        IsAlive = false;
    }
}
