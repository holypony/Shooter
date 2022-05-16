using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class directionPointer : MonoBehaviour
{
    [SerializeField] private GameSetupSo gameSetupSo;
    [SerializeField] private GameObject[] target;

    private int i = 0;

    private void Awake()
    {
        gameSetupSo.Target = target[0].transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.CompareTag("Player"))
        {
            if (i == 3)
            {
                i = 0;
            }
            else
            {
                i++;
            }
                
            gameSetupSo.Target = target[i].transform.position;
            transform.Rotate(0f, 0f, 90f);

        }
        
    }
}
