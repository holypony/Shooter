using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshBuilder = UnityEngine.AI.NavMeshBuilder;
public class CubeLvlManager : PoolBase<Cube>
{
    [Header("Navmesh")]
    [SerializeField] private NavMeshSurface Surface;
    [SerializeField] private float UpdateRate = 0.1f;
    [SerializeField] private float MovementThreshold = 3f;
    [SerializeField] private Vector3 NavMeshSize = new Vector3(20, 20, 20);

    private Vector3 WorldAnchor;
    private NavMeshData NavMeshData;
    private List<NavMeshBuildSource> Sources = new List<NavMeshBuildSource>();

    [SerializeField] private Player Player;
    [Header("Cubes")]
    [SerializeField] private Cube CubePref;
    private List<Cube> Cubes;

    [SerializeField] private int PlaneSquare = 256;
    private Vector2 StartCors;
    private Vector3 cor;
    [SerializeField] private float Side;

    private void Awake()
    {
        Side = Mathf.Sqrt(PlaneSquare) * transform.localScale.x;
        StartCors = new Vector2(-Side / 4, -Side);

        Cubes = InitPool(CubePref, PlaneSquare + 100, true);

        NavMeshData = new NavMeshData();
        NavMesh.AddNavMeshData(NavMeshData);
    }

    public void InitNav()
    {

        BuildNavMesh(false);
        StartCoroutine(CheckPlayerMovement());
    }

    public void MakeLvl()
    {
        StartCoroutine(CubeSpawner());

        IEnumerator CubeSpawner()
        {
            for (int x = 0; x < Side * 2; x += 2)
            {
                for (int z = 0; z < Side * 2; z += 2)
                {
                    cor = new Vector3(StartCors.x + x, -0.5f, StartCors.y + z);
                    Get(Cubes, cor, Quaternion.identity);
                }
                yield return null;
            }
        }
    }

    public void RestartLvl()
    {
        StopAllCoroutines();
        IsFalling = false;

        OffAllObjects(Cubes);
        MakeLvl();
    }

    private bool IsFalling = false;
    public void FallingCubes()
    {
        StartCoroutine(CubeKiller());

        IEnumerator CubeKiller()
        {
            IsFalling = true;
            for (int i = 0; i < Cubes.Count; i++)
            {
                if (Cubes[i].gameObject.activeInHierarchy && RandomInt(33)) Cubes[i].TurnOff();
                yield return null;
            }
            IsFalling = false;
        }
    }

    private bool RandomInt(int chance)
    {
        int rnd = Random.Range(0, 101);
        bool r = (rnd < chance) ? true : false;
        return r;
    }

    private IEnumerator CheckPlayerMovement()
    {
        WaitForSeconds Wait = new WaitForSeconds(UpdateRate);

        while (true)
        {
            if (Vector3.Distance(WorldAnchor, Player.transform.position) > MovementThreshold || IsFalling)
            {
                if (Player.transform.position.y < 0.17f && Player.transform.position.y > -0.99f)
                {
                    BuildNavMesh(true);
                    WorldAnchor = Player.transform.position;
                }
            }
            yield return Wait;
        }
    }

    private void BuildNavMesh(bool Async)
    {
        //if (!Player.isGrounded) return;

        Bounds navMeshBounds = new Bounds(Player.transform.position, NavMeshSize);
        List<NavMeshBuildMarkup> markups = new List<NavMeshBuildMarkup>();

        if (Surface.collectObjects == CollectObjects.Children)
        {
            NavMeshBuilder.CollectSources(transform, Surface.layerMask, Surface.useGeometry, Surface.defaultArea, markups, Sources);
        }
        else
        {
            NavMeshBuilder.CollectSources(navMeshBounds, Surface.layerMask, Surface.useGeometry, Surface.defaultArea, markups, Sources);
        }

        Sources.RemoveAll(source => source.component != null && source.component.gameObject.GetComponent<NavMeshAgent>() != null);

        if (Async)
        {
            NavMeshBuilder.UpdateNavMeshDataAsync(NavMeshData, Surface.GetBuildSettings(), Sources, new Bounds(Player.transform.position, NavMeshSize));
        }
        else
        {
            NavMeshBuilder.UpdateNavMeshData(NavMeshData, Surface.GetBuildSettings(), Sources, new Bounds(Player.transform.position, NavMeshSize));
        }
    }
}
