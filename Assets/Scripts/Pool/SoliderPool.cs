using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoliderPool : PoolBase<Bot>
{
    private List<Bot> SoldierPool;
    [SerializeField] private Bot EnemySoldierPref;
    private int _soldiersQuantity = 15;
    [SerializeField] private float timeBetweenSpawn = 1f;
    [SerializeField] private GameObject SoldierSpawnPoint;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject Target;
    
    
    [SerializeField] private GameSetupSo gameSetupSo;

    private Vector3 startPlayerPos;
    void Awake()
    {
        gameSetupSo.IsPlay = false;
    }

    public void StartGame()
    {
        
        startPlayerPos = Player.transform.position;
        SoldierPool = InitPool(EnemySoldierPref, _soldiersQuantity);
        Spawn();
    }

    public void Spawn()
    {

        Player.transform.position = startPlayerPos;
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
