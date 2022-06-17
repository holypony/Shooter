using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiRadar : MonoBehaviour
{

    [SerializeField] private SoliderPool soldierPool;
    public float distToTarget;

    public GameObject target = null;
    void Start()
    {
        InvokeRepeating("SelectTarget", 0f, 0.25f);
    }
    private float distToEnemy;
    private void SelectTarget()
    {
        distToTarget = 99f;
        for (int i = 0; i < soldierPool.SoldierPool.Count; i++)
        {
            if (soldierPool.SoldierPool[i].IsAlive)
            {
                distToEnemy = Vector3.Distance(transform.position, soldierPool.SoldierPool[i].transform.position);
                if (distToTarget > distToEnemy)
                {
                    distToTarget = distToEnemy;
                    target = soldierPool.SoldierPool[i].gameObject;
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            //SoldierPool.Add(collision.gameObject);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            //SoldierPool.Remove(other.gameObject);
        }
    }
}
