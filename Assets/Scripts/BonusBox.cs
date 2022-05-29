
using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;
using DG.Tweening;

public class BonusBox : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private bool isAmmoBox = false;
    [SerializeField] private bool isRocketBox = false;
    [SerializeField] private bool isTruckBox = false;
    [SerializeField] private bool isMineBox = false;
    [SerializeField] private GameSetupSo gameSetupSo;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Visual Setup")]
    [SerializeField] private Sprite ammoSprite;
    [SerializeField] private Sprite mineSprite;

    [SerializeField] private Material mineMat;
    [SerializeField] private Material ammoMat;
    [SerializeField] private Material truckMat;
    [SerializeField] private Material rocketMat;
    [SerializeField] private GameObject MinePrefab;

    [Header("Debug")]
    [SerializeField] private bool isDebug;

    private MeshRenderer mr;
    private void Awake()
    {
        mr = GetComponent<MeshRenderer>();

        transform.DOScale(1f, 1f).SetLoops(-1, LoopType.Yoyo);
        //transform.DORotate(Vector3.right);
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, 5f, 0);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isAmmoBox) gameSetupSo.Bullets += 30;
            if (isMineBox)
            {
                Instantiate(MinePrefab, transform.position, Quaternion.identity);
            }
            if (isTruckBox)
            {
                gameSetupSo.Truck++;
            }

            if (isRocketBox)
            {
                gameSetupSo.Rockets += 5;
            }
            gameObject.SetActive(false);
        }
    }

    public void Init()
    {
        isAmmoBox = false;
        isRocketBox = false;
        isTruckBox = false;
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
        var i = Random.Range(0, 4);

        switch (i)
        {
            case 0:
                isAmmoBox = true;
                mr.material = ammoMat;
                spriteRenderer.sprite = ammoSprite;
                break;
            case 1:
                isTruckBox = true;
                mr.material = truckMat;
                spriteRenderer.sprite = ammoSprite;
                break;
            case 2:
                isMineBox = true;
                mr.material = mineMat;
                spriteRenderer.sprite = mineSprite;
                break;
            case 3:
                isRocketBox = true;
                mr.material = rocketMat;
                spriteRenderer.sprite = mineSprite;
                break;
        }
    }
}
