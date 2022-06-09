using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class RoboSpider : MonoBehaviour, IStationStateSwitcher
{
    [SerializeField] private GameObject _player;
    [SerializeField] private float distToPlayer;

    [Header("Moduls")]
    [SerializeField] private AiRadar _aiRadar;
    [SerializeField] private AiWeapon _aiWeapon;

    private AI _currentState;
    private List<AI> _allStates;
    private NavMeshAgent _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {

        _allStates = new List<AI>()
        {
            new AttackState(this, _aiWeapon, _agent, this),
            new DefenseState(this, _aiWeapon, _agent, this)
        };
        _currentState = _allStates[1];

        InvokeRepeating("CheckPlayerDist", 0f, 1f);

    }

    private void CheckPlayerDist()
    {
        distToPlayer = Vector3.Distance(transform.position, _player.transform.position);
        //distToEnemy
        //distToLoot
    }

    public void Attack(GameObject Target)
    {
        _currentState.Attack(Target);
    }

    public void Following()
    {
        _currentState.Following(_player);
    }

    public void Looting()
    {
        _currentState.Looting();
    }

    public void SwitchState<T>() where T : AI
    {
        var state = _allStates.FirstOrDefault(s => s is T);

        _currentState.Stop();
        state.Start();

        _currentState = state;
    }

    private void Update()
    {
        if (distToPlayer > 3f) Following();
        if (_aiRadar.distToTarget < 6f) Attack(_aiRadar.target);
        /*

        0. be closer than 5f
        1. if PlayerToEnemy < 2 Fire
        2. else looting near 5f
        */
    }
}
