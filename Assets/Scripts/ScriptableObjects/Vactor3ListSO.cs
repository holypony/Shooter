using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Vector3")]
public class Vactor3ListSO : ScriptableObject
{
    [SerializeField] private List<Vector3> pos;

    public List<Vector3> Pos
    {
        get => pos;
        set
        {
            pos = value;
        }
    }
}