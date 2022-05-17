
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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

    [Header("Other")]
    [SerializeField] private GameSetupSo gameSetupSo;
    [SerializeField] private BonusManager _bonusManager;
    void Awake()
    {
        SoldierPool = InitPool(EnemySoldierPref, _soldiersPoolCapacity);
    }

    private void StartGame(bool isPlay)
    {
        if(!isPlay) return;
        Spawn();
    }

    private void Spawn()
    {
        StartCoroutine(SpawnSoldier());
        IEnumerator SpawnSoldier()
        {
            var soldierspawned = 0;
            while (gameSetupSo.IsPlay)
            {
                var dif = soldierspawned - gameSetupSo.Kills;
                if (dif < 5)
                {
                    var i = 0;
                    while (i < _soldiersToSpawn)
                    {
                        soldierspawned++;
                        i++;
                        var position = SoldierSpawnPoint.transform.position;
                        var randomSpawnPos = new Vector3((position.x + Random.Range(0f, 5f)), 0f, (position.z + Random.Range(0f, 5f)));
                        var enemySoldier = Get(SoldierPool, randomSpawnPos, Quaternion.identity);
                        enemySoldier.bonusManager = _bonusManager;
                        enemySoldier.Init();

                        yield return new WaitForSeconds(timeBetweenSpawn);
                    }
                }
                else
                {
                    yield return new WaitForSeconds(timeBetweenSpawn);
                }
                
            }
        }
    }

    private void OnEnable()
    {
        gameSetupSo.OnIsPlayChange += StartGame;
    }

    private void OnDisable()
    {
        gameSetupSo.OnIsPlayChange -= StartGame;
    }
}
