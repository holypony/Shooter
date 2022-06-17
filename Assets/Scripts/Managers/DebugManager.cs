using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DebugManager : MonoBehaviour
{

    [SerializeField] private TMP_Text fpsText;


    private float _timer;
    [SerializeField] private float _hudRefreshRate = 1f;

    void Awake()
    {
        fpsText.text = "";
    }
    void Update()
    {

        if (Time.unscaledTime > _timer)
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            fpsText.text = "FPS: " + fps;
            _timer = Time.unscaledTime + _hudRefreshRate;
        }
    }
}
