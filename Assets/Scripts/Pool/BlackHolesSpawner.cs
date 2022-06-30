using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHolesSpawner : PoolBase<BlackHole>
{
    private List<BlackHole> BlackHole;

    [SerializeField] private BlackHole BlackHolePref;
    [SerializeField] private PolygonCollider2D hole2dPref;
    [SerializeField] private int _blackHoleCapacity = 5;
    [SerializeField] private Vector2 spawnRadius = new Vector2(15f, 15f);
    [SerializeField] private float timeBetweenSpawn = 1f;
    [SerializeField] private GameSetupSo gameSetupSo;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private Player player;
    public List<PolygonCollider2D> hole2dColliders;
    public PolygonCollider2D ground2dColliders;
    public MeshCollider GeneratedMeshColliders;
    Mesh GeneratedMesh;
    public int activeHolesQuantity = 0;

    private void Awake()
    {
        Init2dColPool();
        BlackHole = InitPool(BlackHolePref, _blackHoleCapacity, true);
        Make3DMeshCollider();
    }
    Vector2[] PointPositions;
    private void MakeHole2D(int holeIndex, Vector3 spawnPos)
    {
        hole2dColliders[holeIndex].gameObject.SetActive(true);
        hole2dColliders[holeIndex].transform.position = new Vector2(spawnPos.x, spawnPos.z);

        PointPositions = hole2dColliders[holeIndex].GetPath(0);
        for (int i = 0; i < PointPositions.Length; i++)
        {
            PointPositions[i] += (Vector2)hole2dColliders[holeIndex].transform.position;
        }

        ground2dColliders.pathCount = activeHolesQuantity + 2;
        ground2dColliders.SetPath(activeHolesQuantity + 1, PointPositions);
    }

    private void Make3DMeshCollider()
    {
        if (GeneratedMesh != null) Destroy(GeneratedMesh);
        GeneratedMesh = ground2dColliders.CreateMesh(true, true);
        GeneratedMeshColliders.sharedMesh = GeneratedMesh;
    }

    private void MakeHole()
    {

        Vector3 spawnPos = GetRandomPos();

        SpawnBh(spawnPos);

        MakeHole2D(activeHolesQuantity, spawnPos);

        Make3DMeshCollider();

        activeHolesQuantity++;
    }

    public void CloseHole()
    {
        hole2dColliders[activeHolesQuantity - 1].gameObject.SetActive(false);
        SetOffObj(activeHolesQuantity - 1);
        ground2dColliders.pathCount = activeHolesQuantity;
        Make3DMeshCollider();
        activeHolesQuantity--;

    }


    private void StartGame(bool isPlay)
    {
        if (isPlay)
        {
            StartCoroutine(BhSpawnerRoutine());
        }
        else
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator BhSpawnerRoutine()
    {
        while (gameSetupSo.IsPlay && activeHolesQuantity < _blackHoleCapacity)
        {
            yield return new WaitForSeconds(timeBetweenSpawn);
            MakeHole();
        }

    }

    public void SpawnBh(Vector3 spawnPos)
    {
        var BH = Get(BlackHole, spawnPos, Quaternion.identity);
        BH.Init();
    }

    private Vector3 GetRandomPos()
    {
        var pos = RandomPointInAnnulus(spawnRadius.x, spawnRadius.y);
        var randomSpawnPos = new Vector3(pos.x, 0f, pos.y);
        return randomSpawnPos;
    }
    private Vector2 RandomPointInAnnulus(float minRadius, float maxRadius)
    {
        var position = player.transform.position;
        var origin = new Vector2(position.x + 0.1f, position.z - 0.1f);
        var randomDirection = (Random.insideUnitCircle * origin).normalized;
        var randomDistance = Random.Range(minRadius, maxRadius);
        var point = origin + randomDirection * randomDistance;
        return point;
    }

    private void OnEnable()
    {
        gameSetupSo.OnIsPlayChange += StartGame;
    }

    private void OnDisable()
    {
        gameSetupSo.OnIsPlayChange -= StartGame;
    }

    private void Init2dColPool()
    {
        for (int i = 0; i < _blackHoleCapacity; i++)
        {
            var a = Instantiate(hole2dPref);
            a.gameObject.SetActive(false);
            hole2dColliders.Add(a);
        }
    }
}
