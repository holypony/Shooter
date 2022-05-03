using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bot : MonoBehaviour
{
    //[HideInInspector] public Rigidbody Body;
    public bool isTargeted = false;
    public bool isAlive = false;


    public bool IsTargeted
    {
        get => isTargeted;
        set => isTargeted = value;
    }
    
    public bool IsAlive
    {
        get => isAlive;
        set => isAlive = value;
    }
}
