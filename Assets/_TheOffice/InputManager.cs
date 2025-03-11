using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public static PlayerInput PlayerInput;

    public static Vector2 Movement;
    public static bool JumpWasPressed;
    public static bool JumpIsHeld;
    public static bool JumpIsReleased;
    public static bool RunIsHeld;
    public static bool Attack;

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _runAction;
    private InputAction _attackAction;



    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();
        _moveAction = PlayerInput.actions["Move"];
        _jumpAction = PlayerInput.actions["Jump"];
        _runAction = PlayerInput.actions["Run"];
        _attackAction = PlayerInput.actions["Attack"];

    }

  
    void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();

        Attack = _attackAction.WasPressedThisFrame();

        JumpWasPressed = _jumpAction.WasPressedThisFrame();
        JumpIsHeld = _jumpAction.IsPressed();
        JumpIsReleased = _jumpAction.WasReleasedThisFrame();

        RunIsHeld = _runAction.IsPressed();

    }
}
