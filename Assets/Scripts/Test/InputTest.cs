using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    [SerializeField] private PlayerInput testControlPlayerInput;
    [SerializeField] private InputActionReference 
        platformMoveIA, jumpIA, flyingMoveIA, shootIA, clawMoveIA, grabIA;

    // Start is called before the first frame update
    void Start()
    {
        platformMoveIA.action.performed += TestPlatformMove;
        flyingMoveIA.action.performed += TestFlyingMove;
        clawMoveIA.action.performed += TestClawMove;

        jumpIA.action.started += TestJump;
        shootIA.action.started += TestShoot;
        grabIA.action.started += TestGrab;
    }

    private void TestGrab(InputAction.CallbackContext obj)
    {
        Debug.Log("Grabbing");
    }

    private void TestShoot(InputAction.CallbackContext obj)
    {
        Debug.Log("Shooting");
    }

    private void TestJump(InputAction.CallbackContext obj)
    {
        Debug.Log("Jumping");
    }

    private void TestClawMove(InputAction.CallbackContext obj)
    {
        Debug.Log("Moving Claw " + obj.ReadValue<float>());
    }

    private void TestFlyingMove(InputAction.CallbackContext obj)
    {
        Debug.Log("Moving Fly " + obj.ReadValue<Vector2>());
    }

    private void TestPlatformMove(InputAction.CallbackContext obj)
    {
        Debug.Log("Moving Platformer " + obj.ReadValue<float>());
    }
    
}
