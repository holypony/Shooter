using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bot : MonoBehaviour
{
    public GameObject PlayerTarget;
    public bool isAlive = false;
    public BonusManager bonusManager;
    public virtual void Init()
    {
        IsAlive = true;
    }

    public bool IsAlive
    {
        get => isAlive;
        set => isAlive = value;
    }
}
