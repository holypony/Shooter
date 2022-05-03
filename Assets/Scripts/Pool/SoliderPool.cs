using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoliderPool : PoolBase<Bot>
{
    private List<Bot> SoldierPool;
    [SerializeField] private Bot EnemySoldierPref;
    private int _soldiersQuantity = 5;
    
    
    void Start()
    {
        SoldierPool = InitPool(EnemySoldierPref, _soldiersQuantity);
    }

    
}
