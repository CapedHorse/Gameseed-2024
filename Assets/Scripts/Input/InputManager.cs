using System;
using System.Collections;
using System.Collections.Generic;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;

    [SerializeField] private List<InputMapWrapper> inputMapWrappers;
    
    

    public void EnablePlayerInput()
    {
        playerInput.ActivateInput();
    }

    public void DisablePlayerInput()
    {
        playerInput.DeactivateInput();
    }
}
