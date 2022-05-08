using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class ActionController : MonoBehaviour
{
    private PlayerInput playerInput;
    public Vector2 Move { get; private set; }
    public Vector2 Aim { get; private set; }
    
    public bool IsAiming { get; private set; }
    
    private bool _fireBool = false;
    public event Action<bool> OnFireBoolChange;
    public bool FireBool
    {
        get => _fireBool;
        set
        {
            _fireBool = value;
            OnFireBoolChange?.Invoke(_fireBool);
        }
    }
   
    
    private void Awake()
    {
        playerInput = new PlayerInput(); 
    }
    
    private void Update()
    {
        //Debug.Log(playerInput.Touch.Joystick.ReadValue<Vector2>());
        Move = playerInput.PlayerMap.MoveStick.ReadValue<Vector2>();
        Aim = playerInput.PlayerMap.AimStick.ReadValue<Vector2>();
        //cameraLook = playerInput.PlayerMap.JoystickCameraMove.ReadValue<Vector2>();
        //transform.Rotate(-Y, 0f, 0f, Space.Self);
        //transform.Rotate(0f, X, 0f, Space.World);

    }
    
    private void Start()
    {
        playerInput.PlayerMap.FireButton.started += PressFireBtn;
        playerInput.PlayerMap.FireButton.canceled += UnpressFireBtn;

        playerInput.PlayerMap.AimStick.started += StartAiming;
        playerInput.PlayerMap.AimStick.canceled += StopAiming;
    }
    
    private void StartAiming(InputAction.CallbackContext ctx)
    {
        IsAiming = true;
    }
    private void StopAiming(InputAction.CallbackContext ctx)
    {
        IsAiming = false;
    }

    private void PressFireBtn(InputAction.CallbackContext ctx)
    {
        FireBool = true;
    }
    
    private void UnpressFireBtn(InputAction.CallbackContext ctx)
    {
        FireBool = false;
    }

    


    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
}
