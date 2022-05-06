using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoliderPool : PoolBase<Bot>
{
    private List<Bot> SoldierPool;
    [SerializeField] private Bot EnemySoldierPref;
    private int _soldiersQuantity = 5;
    [SerializeField] private float timeBetweenSpawn = 1f;
    [SerializeField] private GameObject SoldierSpawnPoint;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Target;
    
    void Start()
    {
        SoldierPool = InitPool(EnemySoldierPref, _soldiersQuantity);

        StartCoroutine(SpawnSoldier());
        IEnumerator SpawnSoldier()
        {
            yield return new WaitForSeconds(timeBetweenSpawn * 2f);
            int i = 0;
            while (i < 15)
            {
                i++;
                var enemySoldier = Get(SoldierPool, SoldierSpawnPoint.transform.position, Quaternion.identity);
                enemySoldier.player = Player;
                enemySoldier.target = Target;
                enemySoldier.Init();

                yield return new WaitForSeconds(timeBetweenSpawn);
            }
            
        }
        
    }

    
}
