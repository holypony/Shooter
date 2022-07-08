
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoliderPool : PoolBase<Bot>
{
    public List<Bot> SoldierPool;
    [Header("Soldier spawn setting")]
    [SerializeField] private Bot EnemySoldierPref;
    [SerializeField] private int _soldiersPoolCapacity = 1;
    [SerializeField] private int _soldiersToSpawn = 15;
    [SerializeField] private float timeBetweenSpawn = 1f;
    [SerializeField] private Vector2 spawnRadius = new Vector2(15f, 15f);
    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 SpawnPointStart;
    [SerializeField] private Vector3 SpawnPointEnd;

    [Header("Other")]
    [SerializeField] private GameSetupSo gameSetupSo;
    [SerializeField] private PlayerSO playerSO;
    [SerializeField] private BonusManager _bonusManager;
    void Awake()
    {
        SoldierPool = InitPool(EnemySoldierPref, _soldiersPoolCapacity);
    }


    public void StartSpawn()
    {
        soldierSpawned = 0;
        StartCoroutine(Spawn());
    }

    public void KillAll()
    {
        OffAllObjects(SoldierPool);
    }



    [SerializeField] private int soldierAlive = 0;
    [SerializeField] private int soldierSpawned = 0;
    private IEnumerator Spawn()
    {
        WaitForSeconds Wait = new WaitForSeconds(timeBetweenSpawn);
        while (gameSetupSo.IsPlay)
        {
            while (gameSetupSo.IsPause) yield return new WaitForSeconds(1f);
            soldierAlive = soldierSpawned - playerSO.Kills;
            if (soldierAlive < _soldiersToSpawn)
            {
                soldierSpawned++;
                //SpawnSoldier(GetRandomPos());
                Vector3 pos = new Vector3(Random.RandomRange(-5f, 5f), 1f, Random.RandomRange(30f, 40f));
                SpawnSoldier(pos);
            }
            yield return Wait;
        }

    }

    private Vector3 GetRandomPos()
    {
        var pos = RandomPointInAnnulus(spawnRadius.x, spawnRadius.y);
        var randomSpawnPos = new Vector3(pos.x, 0f, pos.y);
        //if (pos.x > 70 || pos.x < -70) randomSpawnPos = GetRandomPos();
        //if (pos.y > 70 || pos.y < -70) randomSpawnPos = GetRandomPos();

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
        //gameSetupSo.OnIsPlayChange += StartGame;
    }

    private void OnDisable()
    {
        //gameSetupSo.OnIsPlayChange -= StartGame;
    }


    private Vector2 RandomPointInAnnulus(float minRadius, float maxRadius)
    {
        var position = target.transform.position;
        var origin = new Vector2(position.x + 0.1f, position.z - 0.1f);
        var randomDirection = (Random.insideUnitCircle * origin).normalized;
        var randomDistance = Random.Range(minRadius, maxRadius);
        var point = origin + randomDirection * randomDistance;
        return point;
    }
}
