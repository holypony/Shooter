using UnityEngine;
using DG.Tweening;
public class MiningObj : MonoBehaviour, IMineral
{
    public int crystalAmount = 4;

    void OnEnable()
    {
        Init();
    }
    public void Init()
    {
        crystalAmount = 4;

    }
    void Start()
    {
        //transform.DORotate(new Vector3(0f, 45f, 0f), 2f, RotateMode.Fast).SetLoops(-1).SetEase(Ease.Linear);
        transform.DOScale(1.2f, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    public void Depletion()
    {
        gameObject.SetActive(false);
    }

    public bool Mining()
    {
        if (crystalAmount <= 1)
        {
            Invoke("Depletion", 0.1f);
            return false;
        }
        else
        {
            crystalAmount--;
            return true;
        }
    }
}
