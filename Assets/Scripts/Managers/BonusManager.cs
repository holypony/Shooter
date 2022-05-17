using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : PoolBase<BonusBox>
{
    private List<BonusBox> BoxPool;
    [Header("Soldier spawn setting")]
    [SerializeField] private BonusBox BoxPref;
    [SerializeField] private int _boxesPoolCapacity= 5;

    void Awake()
    {
        BoxPool = InitPool(BoxPref, _boxesPoolCapacity);
    }

    public void SpawnBox(Vector3 spawnPos)
    {
        
        var enemySoldier = Get(BoxPool, spawnPos, Quaternion.identity);
    }
}
