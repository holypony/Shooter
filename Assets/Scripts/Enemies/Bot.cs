using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bot : MonoBehaviour
{
    //[HideInInspector] public Rigidbody Body;
    public bool isTargeted = false;
    public bool isAlive = false;
    public GameObject player;
    public GameObject target;

    public bool IsTargeted
    {
        get => isTargeted;
        set => isTargeted = value;
    }

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
