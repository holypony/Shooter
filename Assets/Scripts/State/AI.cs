using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AI : MonoBehaviour
{

    protected readonly AiWeapon _aiWeapon;
    protected readonly IStationStateSwitcher _stateSwitcher;
    protected readonly NavMeshAgent _agent;
    protected readonly RoboSpider _roboSpider;

    protected AI(RoboSpider roboSpider, AiWeapon aiWeapon, NavMeshAgent agent, IStationStateSwitcher stationStateSwitcher)

    {
        _roboSpider = roboSpider;
        _aiWeapon = aiWeapon;
        _agent = agent;
        _stateSwitcher = stationStateSwitcher;
    }



    public abstract void Start();
    public abstract void Stop();

    public abstract void Following(GameObject player);
    public abstract void Looting();
    public abstract void Attack(GameObject Target);



}
