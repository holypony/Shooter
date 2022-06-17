using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;

public class FireBaseAnalytics : MonoBehaviour
{
    private DependencyStatus dependencyStatus;

    private void Start()
    {


        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase

            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }
}
