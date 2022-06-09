using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : AI
{
    public AttackState(RoboSpider roboSpider, AiWeapon aiWeapon, NavMeshAgent agent, IStationStateSwitcher stationStateSwitcher) : base(roboSpider, aiWeapon, agent, stationStateSwitcher)
    {

    }

    public override void Start()
    {

    }

    public override void Stop()
    {

    }

    public override void Attack(GameObject Target)
    {
        //
    }

    public override void Following(GameObject player)
    {
        //_stateSwitcher.SwitchState<AttackState>();
        _agent.SetDestination(player.transform.position);
    }

    public override void Looting()
    {
        //
    }
}
