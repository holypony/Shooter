using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bot : MonoBehaviour
{

    private bool isAlive = false;
    
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
