
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
    [SerializeField] private Vector2 spawnRadius = new Vector2(10f,15f);

    [SerializeField] private GameObject target;


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
        offAllObjects(SoldierPool);
        Spawn();
        
    }

    [SerializeField] private int soldierAlive = 0;
    [SerializeField] private int soldierSpawned = 0;
    private void Spawn()
    {
        StartCoroutine(Spawner());
        
        IEnumerator Spawner()
        {
            while (gameSetupSo.IsPlay)
            {
                soldierAlive = soldierSpawned - gameSetupSo.Kills;
                
                if (soldierAlive < _soldiersToSpawn)
                {
                    soldierSpawned++;
                    SpawnSoldier(GetRandomPos());
                }
                yield return new WaitForSeconds(timeBetweenSpawn);
            }
        }
    }

    private Vector3 GetRandomPos()
    {
        var pos = RandomPointInAnnulus(spawnRadius.x,spawnRadius.y);
        var randomSpawnPos = new Vector3(pos.x, 0f, pos.y);
        return randomSpawnPos;
    }

    private void SpawnSoldier(Vector3 randomSpawnPos)
    {
        var enemySoldier = Get(SoldierPool, randomSpawnPos, Quaternion.identity);
        enemySoldier.bonusManager = _bonusManager;
        enemySoldier.PlayerTarget = target;
        enemySoldier.Init();
        
    }
    
    private void OnEnable()
    {
        gameSetupSo.OnIsPlayChange += StartGame;
    }

    private void OnDisable()
    {
        gameSetupSo.OnIsPlayChange -= StartGame;
    }


    private Vector2 RandomPointInAnnulus(float minRadius, float maxRadius){
        
        var position = target.transform.position;
        var origin = new Vector2(position.x + 0.1f, position.z - 0.1f);
        
        var randomDirection = (Random.insideUnitCircle * origin).normalized;
 
        var randomDistance = Random.Range(minRadius, maxRadius);
 
        var point = origin + randomDirection * randomDistance;
 
        return point;
    }
}
