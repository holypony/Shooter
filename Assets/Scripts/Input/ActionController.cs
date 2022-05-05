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
    public Vector2 cameraLook { get; private set; }
    
    public bool RocketFireBool { get; private set; }
    
    private bool _fireBool = false;
    public event Action<bool> OnFireBoolChange;
    public bool FireBool
    {
        get => _fireBool;
        private set
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
        //cameraLook = playerInput.PlayerMap.JoystickCameraMove.ReadValue<Vector2>();
        //transform.Rotate(-Y, 0f, 0f, Space.Self);
        //transform.Rotate(0f, X, 0f, Space.World);

    }
    
    private void Start()
    {
        playerInput.PlayerMap.FireButton.started += ctx => pressFireBtn(ctx);
        playerInput.PlayerMap.FireButton.canceled += ctx => unpressFireBtn(ctx);

        //playerInput.PlayerMap.RocketBtn.started += ctx => pressLandFireBtn(ctx);
        //playerInput.PlayerMap.RocketBtn.canceled += ctx => unpressLandFireBtn(ctx);
    }

    private void pressFireBtn(InputAction.CallbackContext ctx)
    {
        FireBool = true;
    }
    
    private void unpressFireBtn(InputAction.CallbackContext ctx)
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
