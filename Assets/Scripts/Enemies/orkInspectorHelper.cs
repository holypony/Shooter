using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(EnemySoldier))]
public class orkInspectorHelper : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EnemySoldier enemySoldier = (EnemySoldier)target;
        if (GUILayout.Button("Save ork postion"))
        {
            enemySoldier.SaveLocalPos();
        }
    }
}
