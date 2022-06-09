using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DefenseState : AI
{

    public DefenseState(RoboSpider roboSpider, AiWeapon aiWeapon, NavMeshAgent agent, IStationStateSwitcher stationStateSwitcher) : base(roboSpider, aiWeapon, agent, stationStateSwitcher)
    {

    }

    public override void Start()
    {

    }

    public override void Stop()
    {

    }

    public override void Attack(GameObject target)
    {
        _roboSpider.transform.LookAt(target.transform);
        _aiWeapon.Shoot();

    }

    public override void Following(GameObject player)
    {
        //_stateSwitcher.SwitchState<AttackState>();
        _agent.SetDestination(player.transform.position);
    }

    public override void Looting()
    {
        // if(distToLoot < 5f)
    }


}
