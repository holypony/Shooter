using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshBuilder = UnityEngine.AI.NavMeshBuilder;
public struct groundPanel
{
    public GameObject prefab;
    public Vector4 coors;
}

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private NavMeshSurface Surface;
    [SerializeField]
    private float UpdateRate = 0.1f;
    [SerializeField]
    private float MovementThreshold = 3f;
    [SerializeField]
    private Vector3 NavMeshSize = new Vector3(20, 20, 20);

    private Vector3 WorldAnchor;
    private NavMeshData NavMeshData;
    private List<NavMeshBuildSource> Sources = new List<NavMeshBuildSource>();


    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject planePref;
    [SerializeField] private GameSetupSo gameSetupSo;

    public groundPanel[] groundPanelsArr;

    private float x1, x2, y1, y2;
    public float halfPlaneSide;
    public int ActivePlaneIndex;

    public BlackHolesSpawner blackHolesSpawner;

    private void Awake()
    {
        //InitPlaneManager();

        NavMeshData = new NavMeshData();
        NavMesh.AddNavMeshData(NavMeshData);
        BuildNavMesh(false);
        StartCoroutine(CheckPlayerMovement());

        //groundPanelsArr[0].prefab.GetComponent<MeshCollider>().enabled = false;
    }

    private void InitPlaneManager()
    {

        groundPanelsArr = new groundPanel[6];

        halfPlaneSide = planePref.transform.localScale.x * 5f;

        for (int i = 0; i < groundPanelsArr.Length; i++)
        {
            planePref.SetActive(false);
            groundPanelsArr[i].prefab = Instantiate(planePref, transform.position, Quaternion.identity, gameObject.transform);

            groundPanelsArr[i].coors = new Vector4();

        }
        groundPanelsArr[0].prefab.transform.position = Vector3.zero;
        groundPanelsArr[0].prefab.SetActive(true);

        AddPanelSquareCor(0);

        StartCoroutine(FastCheck());
        StartCoroutine(LongCheck());
    }



    private IEnumerator FastCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            CheckOffset(ActivePlaneIndex, 30f);
        }
    }

    private IEnumerator LongCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            CheckActivePlane(Player.transform.position, true);
            for (int i = 0; i < groundPanelsArr.Length; i++)
            {
                if (groundPanelsArr[i].prefab.activeSelf)
                {
                    float dist = Vector3.Distance(Player.transform.position, groundPanelsArr[i].prefab.transform.position);
                    if (dist > 99f)
                    {
                        Debug.Log("dist " + dist);
                        groundPanelsArr[i].prefab.SetActive(false);
                        groundPanelsArr[i].coors = new Vector4(0f, 0f, 0f, 0f);
                    }
                }
            }
        }
    }

    private void AddPanelSquareCor(int planeIndex)
    {

        x1 = groundPanelsArr[planeIndex].prefab.transform.position.x - halfPlaneSide;
        y1 = groundPanelsArr[planeIndex].prefab.transform.position.z - halfPlaneSide;
        x2 = groundPanelsArr[planeIndex].prefab.transform.position.x + halfPlaneSide;
        y2 = groundPanelsArr[planeIndex].prefab.transform.position.z + halfPlaneSide;

        groundPanelsArr[planeIndex].coors = (new Vector4(x1, y1, x2, y2));
    }

    private bool CheckActivePlane(Vector3 TargetPos, bool FindActivePanel = true)
    {
        for (int i = 0; i < groundPanelsArr.Length; i++)
        {
            if (TargetPos.x > groundPanelsArr[i].coors.x && TargetPos.x < groundPanelsArr[i].coors.z)
            {
                if (TargetPos.z > groundPanelsArr[i].coors.y && TargetPos.z < groundPanelsArr[i].coors.w)
                {
                    if (FindActivePanel)
                    {

                        ActivePlaneIndex = i;
                    }

                    return true;
                }
            }
        }
        return false;
    }

    private Vector3 newPlanePos;
    private void CheckOffset(int PlaneIndex = 0, float offset = 0)
    {
        if (Player.transform.position.x < groundPanelsArr[PlaneIndex].coors.x + offset)
        {
            newPlanePos = new Vector3(groundPanelsArr[ActivePlaneIndex].prefab.transform.position.x - groundPanelsArr[ActivePlaneIndex].prefab.transform.localScale.x * 10, 0f, groundPanelsArr[ActivePlaneIndex].prefab.transform.position.z);
            SpawnNewPlane(newPlanePos);
            Debug.Log("Init plane left");

            if (Player.transform.position.z < groundPanelsArr[PlaneIndex].coors.y + offset)
            {
                newPlanePos = new Vector3(groundPanelsArr[ActivePlaneIndex].prefab.transform.position.x - groundPanelsArr[ActivePlaneIndex].prefab.transform.localScale.x * 10, 0f, groundPanelsArr[ActivePlaneIndex].prefab.transform.position.z - groundPanelsArr[ActivePlaneIndex].prefab.transform.localScale.x * 10);
                SpawnNewPlane(newPlanePos);
            }
            if (Player.transform.position.z > groundPanelsArr[PlaneIndex].coors.w - offset)
            {
                newPlanePos = new Vector3(groundPanelsArr[ActivePlaneIndex].prefab.transform.position.x - groundPanelsArr[ActivePlaneIndex].prefab.transform.localScale.x * 10, 0f, groundPanelsArr[ActivePlaneIndex].prefab.transform.position.z + groundPanelsArr[ActivePlaneIndex].prefab.transform.localScale.x * 10);
                SpawnNewPlane(newPlanePos);
            }
        }

        if (Player.transform.position.x > groundPanelsArr[PlaneIndex].coors.z - offset)
        {
            newPlanePos = new Vector3(groundPanelsArr[ActivePlaneIndex].prefab.transform.position.x + groundPanelsArr[ActivePlaneIndex].prefab.transform.localScale.x * 10, 0f, groundPanelsArr[ActivePlaneIndex].prefab.transform.position.z);
            SpawnNewPlane(newPlanePos);
            Debug.Log("Init plane right");

            if (Player.transform.position.z < groundPanelsArr[PlaneIndex].coors.y + offset)
            {
                newPlanePos = new Vector3(groundPanelsArr[ActivePlaneIndex].prefab.transform.position.x + groundPanelsArr[ActivePlaneIndex].prefab.transform.localScale.x * 10, 0f, groundPanelsArr[ActivePlaneIndex].prefab.transform.position.z - groundPanelsArr[ActivePlaneIndex].prefab.transform.localScale.x * 10);
                SpawnNewPlane(newPlanePos);
            }
            if (Player.transform.position.z > groundPanelsArr[PlaneIndex].coors.w - offset)
            {
                newPlanePos = new Vector3(groundPanelsArr[ActivePlaneIndex].prefab.transform.position.x + groundPanelsArr[ActivePlaneIndex].prefab.transform.localScale.x * 10, 0f, groundPanelsArr[ActivePlaneIndex].prefab.transform.position.z + groundPanelsArr[ActivePlaneIndex].prefab.transform.localScale.x * 10);
                SpawnNewPlane(newPlanePos);
            }
        }

        if (Player.transform.position.z < groundPanelsArr[PlaneIndex].coors.y + offset)
        {
            newPlanePos = new Vector3(groundPanelsArr[ActivePlaneIndex].prefab.transform.position.x, 0f, groundPanelsArr[ActivePlaneIndex].prefab.transform.position.z - groundPanelsArr[ActivePlaneIndex].prefab.transform.localScale.x * 10);
            SpawnNewPlane(newPlanePos);
            Debug.Log("Init plane bot");
        }

        if (Player.transform.position.z > groundPanelsArr[PlaneIndex].coors.w - offset)
        {
            newPlanePos = new Vector3(groundPanelsArr[ActivePlaneIndex].prefab.transform.position.x, 0f, groundPanelsArr[ActivePlaneIndex].prefab.transform.position.z + groundPanelsArr[ActivePlaneIndex].prefab.transform.localScale.x * 10);
            SpawnNewPlane(newPlanePos);
            Debug.Log("Init plane up");
        }
    }

    private void SpawnNewPlane(Vector3 PlanePos)
    {
        if (CheckActivePlane(PlanePos, false)) return;

        for (int i = 0; i < groundPanelsArr.Length; i++)
        {
            if (!groundPanelsArr[i].prefab.activeSelf)
            {
                groundPanelsArr[i].prefab.transform.position = PlanePos;

                groundPanelsArr[i].prefab.SetActive(true);
                AddPanelSquareCor(i);

                return;
            }
        }
    }

    private IEnumerator CheckPlayerMovement()
    {
        WaitForSeconds Wait = new WaitForSeconds(UpdateRate);

        while (true)
        {

            if (Vector3.Distance(WorldAnchor, Player.transform.position) > MovementThreshold)
            {
                if (Player.transform.position.y < 0.15f && Player.transform.position.y > -0.99f)
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
