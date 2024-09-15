using UnityEngine;
using UnityEngine.InputSystem;

public class TestController : MonoBehaviour
{

    public Rigidbody2D rb;
    public float moveSpeed;
    public float dashPower;
    public InputActionReference move;
    public InputActionReference fire;
    public InputActionReference dash;
    public Animator testAnimator;
    private Vector2 _moveDirection;
    private float dashForce;
    private bool isDashing;
    private bool moveDirToRight;

    public PlayerInput testPlayerInput;
    /*
    private void Update()
    {
        _moveDirection = move.action.ReadValue<Vector2>();
        
    }
    */

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(_moveDirection.x * moveSpeed + GetDashForce(), 0);
    }

    private void OnEnable()
    {
        fire.action.started += Fire;
        move.action.performed += Move;
        move.action.canceled += StopMove;
        dash.action.started += Dash;
        dash.action.canceled += CancelDash;
    }

    private void CancelDash(InputAction.CallbackContext obj)
    {
        isDashing = false;
    }

    private void Dash(InputAction.CallbackContext obj)
    {
        isDashing = true;
        testAnimator.SetTrigger("Dash");
    }
    
    private float GetDashForce()
    {
        if (isDashing)
        {
            return moveDirToRight ? dashPower : -dashPower;
        }

        return 0;
    }

    
    private void StopMove(InputAction.CallbackContext obj)
    {
        _moveDirection = Vector2.zero;
    }

    private void Move(InputAction.CallbackContext obj)
    {
        _moveDirection = obj.ReadValue<Vector2>();

        moveDirToRight = obj.ReadValue<Vector2>().x > 0;
        testAnimator.SetBool("MoveRight", moveDirToRight);
        testAnimator.SetBool("MoveLeft", !moveDirToRight);
    }

    private void Fire(InputAction.CallbackContext obj)
    {
        
    }

    private void OnDisable()
    {
        fire.action.started -= Fire;
        move.action.performed -= Move;
        move.action.canceled -= StopMove;
        dash.action.started -= Dash;
    }
}
