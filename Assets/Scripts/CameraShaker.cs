using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShaker : MonoBehaviour
{

    public static CameraShaker instance;
    [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
    [SerializeField] private CinemachineBasicMultiChannelPerlin cmPerlin;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }
        Destroy(this.gameObject);



    }

    void OnEnable()
    {
        _cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void CameraShake()
    {
        cmPerlin = _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        StartCoroutine(Shaker());
        IEnumerator Shaker()
        {
            cmPerlin.m_AmplitudeGain = 2.5f;
            while (cmPerlin.m_AmplitudeGain > 0)
            {
                cmPerlin.m_AmplitudeGain -= 0.25f;
                yield return new WaitForSeconds(0.1f);
            }

        }
    }
}
