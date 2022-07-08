using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningModule : MonoBehaviour
{
    [Header("Mining setting")]
    [SerializeField] private PlayerSO playerSO;
    private bool isMining = false;
    private WaitForSecondsRealtime Wait = new WaitForSecondsRealtime(0.25f);
    private LineRenderer miningLR;


    void OnTriggerEnter(Collider other)
    {
        if (isMining) return;

        if (other.TryGetComponent<IMineral>(out IMineral mineral))
        {
            StartCoroutine(Mining(mineral));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<IMineral>(out IMineral mineral))
        {
            isMining = false;
        }
    }

    IEnumerator Mining(IMineral CrystalPool)
    {
        isMining = true;
        while (isMining)
        {
            if (!CrystalPool.Mining()) isMining = false;
            playerSO.Crystal++;
            yield return Wait;
        }
    }
}
