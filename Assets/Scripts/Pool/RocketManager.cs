using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketManager : PoolBase<Rocket>
{

    private List<Rocket> RocketPool;
    [Header("Soldier spawn setting")]
    [SerializeField] private Rocket rocketPref;
    [SerializeField] private int _boxesPoolCapacity = 5;

    void Awake()
    {
        RocketPool = InitPool(rocketPref, _boxesPoolCapacity, true);
    }

    public void SpawnRocket(Vector3 spawnPos, Quaternion quaternion)
    {
        var rocket = Get(RocketPool, spawnPos, quaternion);
    }
}
