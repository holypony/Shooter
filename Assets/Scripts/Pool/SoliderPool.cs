using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoliderPool : PoolBase<Bot>
{
    private List<Bot> SoldierPool;
    [Header("Soldier spawn setting")]
    [SerializeField] private Bot EnemySoldierPref;
    [SerializeField] private int _soldiersPoolCapacity= 1;
    [SerializeField] private int _soldiersToSpawn = 15;
    [SerializeField] private float timeBetweenSpawn = 1f;
    [Header("Soldier setup")]
    [SerializeField] private GameObject SoldierSpawnPoint;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Target;
    [Header("Other")]
    [SerializeField] private GameSetupSo gameSetupSo;
    void Awake()
    {
        gameSetupSo.IsPlay = false;
        SoldierPool = InitPool(EnemySoldierPref, _soldiersPoolCapacity);
    }

    public void StartGame()
    {
        
        Spawn();
    }

    public void Spawn()
    {

        StartCoroutine(SpawnSoldier());
        IEnumerator SpawnSoldier()
        {
            yield return new WaitForSeconds(timeBetweenSpawn * 2f);
            var i = 0;
            while (i < _soldiersToSpawn)
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
