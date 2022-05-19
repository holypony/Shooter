
using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class BonusBox : MonoBehaviour
{
    [SerializeField] private bool isAmmoBox = false;
    [SerializeField] private bool isRocketBox = false;
    [SerializeField] private bool isTimeBox = false;
    [SerializeField] private bool isMineBox = false;
    [SerializeField] private GameSetupSo gameSetupSo;
    [SerializeField] private SpriteRenderer spriteRenderer;
    
    [SerializeField] private Sprite ammoSprite;
    [SerializeField] private Sprite mineSprite;
    
    [SerializeField] private GameObject MinePrefab;
    

    private void FixedUpdate()
    {
        transform.Rotate(0,5f,0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(isAmmoBox) gameSetupSo.Bullets += 30;
            if (isMineBox)
            {
                Instantiate(MinePrefab, transform.position, Quaternion.identity);
            }
            gameObject.SetActive(false);
        }
    }

    public void Init()
    {
        isAmmoBox = false;
        isRocketBox = false;
        isTimeBox = false;
        isMineBox = false;
        SetBonus();
        
        StartCoroutine(lifeTime());
        
        IEnumerator lifeTime()
        {
            yield return new WaitForSeconds(7f);
            gameObject.SetActive(false);
            
        }
    }

    private void SetBonus()
    {
        var i = Random.Range(0, 3);

        switch (i)
        {
            case 0:
                isAmmoBox = true;
                spriteRenderer.sprite = ammoSprite;
                break;
            case 1:
                isAmmoBox = true;
                spriteRenderer.sprite = ammoSprite;
                break;
            case 2:
                isMineBox = true;
                spriteRenderer.sprite = mineSprite;
                break;
        }
    }
}
