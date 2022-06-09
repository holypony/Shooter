using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckManager : PoolBase<Vehicles>
{
    private List<Vehicles> TruckPool;
    [Header("Soldier spawn setting")]
    [SerializeField] private Vehicles truckPref;
    [SerializeField] private int _truckPoolCapacity = 5;
    [SerializeField] private GameObject player;
    [SerializeField] private GameSetupSo gameSetupSo;

    void Awake()
    {
        TruckPool = InitPool(truckPref, _truckPoolCapacity, true);
    }

    private Vector3 plus = new Vector3(0, 0, 10);
    private Vector3 minus = new Vector3(0, 0, -10);
    public void SpawnTruck(int tr)
    {
        var truck = Get(TruckPool, Vector3.zero, Quaternion.identity);

        int i = Random.Range(0, 2);
        int t = Random.Range(0, 2);
        if (i == 1)
        {

            truck.transform.position = plus + player.transform.position;
            if (t == 0)
            {
                truck.transform.rotation = Quaternion.Euler(0, 135, 0);
            }
            else
            {
                truck.transform.rotation = Quaternion.Euler(0, 225, 0);
            }
        }
        else
        {
            truck.transform.position = minus + player.transform.position;
            if (t == 0)
            {
                truck.transform.rotation = Quaternion.Euler(0, 45, 0);
            }
            else
            {
                truck.transform.rotation = Quaternion.Euler(0, 315, 0);
            }
        }

        truck.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        gameSetupSo.OnTruckChange += SpawnTruck;
    }

    private void OnDisable()
    {
        gameSetupSo.OnTruckChange -= SpawnTruck;
    }
}
