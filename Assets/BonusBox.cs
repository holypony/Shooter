
using System;
using System.Collections;
using UnityEngine;

public class BonusBox : MonoBehaviour
{
    [SerializeField] private bool isAmmoBox = true;
    [SerializeField] private bool isRocketBox = true;
    [SerializeField] private bool isTimeBox = true;
    [SerializeField] private GameSetupSo gameSetupSo;
    private void Awake()
    {
        StartCoroutine(lifeTime());
        
        IEnumerator lifeTime()
        {
            yield return new WaitForSeconds(3f);
            gameObject.SetActive(false);
            
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(0,5f,0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameSetupSo.Bullets += 1;
            Debug.Log("Player get bonus");
            gameObject.SetActive(false);
        }
        
    }
}
