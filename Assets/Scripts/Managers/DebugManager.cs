using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DebugManager : MonoBehaviour
{

    [SerializeField] private TMP_Text fpsText;
    [SerializeField] private bool DebugMode = false;
    [SerializeField] private GameObject btnVeh;
    [SerializeField] private GameObject btnRocket;

    [SerializeField] private GameSetupSo gameSetupSo;
    // Start is called before the first frame update
    private float _timer;
    [SerializeField] private float _hudRefreshRate = 1f;

    void Awake()
    {

        btnVeh.SetActive(DebugMode);
        btnRocket.SetActive(DebugMode);
        fpsText.text = "";

    }
    void Update()
    {
        if (!DebugMode) return;
        if (Time.unscaledTime > _timer)
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            fpsText.text = "FPS: " + fps;
            _timer = Time.unscaledTime + _hudRefreshRate;
        }
    }

    public void AddVehicle()
    {
        gameSetupSo.Truck++;
    }
    public void AddRocket()
    {
        gameSetupSo.Rockets += 1;
    }
}
