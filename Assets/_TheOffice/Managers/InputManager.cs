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
    public static bool RangeAttack;
    public static bool Escape;

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _runAction;
    private InputAction _attackAction;
    private InputAction _rangedAttack;
    private InputAction _escape;



    private void Awake()
    {
        PlayerInput = GetComponent<PlayerInput>();
        _moveAction = PlayerInput.actions["Move"];
        _jumpAction = PlayerInput.actions["Jump"];
        _runAction = PlayerInput.actions["Run"];
        _attackAction = PlayerInput.actions["Attack"];
        _rangedAttack = PlayerInput.actions["RangeAttack"];
        _escape = PlayerInput.actions["Escape"];

    }

  
    void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();

        Attack = _attackAction.WasPressedThisFrame();

        Escape = _escape.WasPressedThisFrame();

        RangeAttack = _rangedAttack.WasPressedThisFrame();



        JumpWasPressed = _jumpAction.WasPressedThisFrame();
        JumpIsHeld = _jumpAction.IsPressed();
        JumpIsReleased = _jumpAction.WasReleasedThisFrame();

        RunIsHeld = _runAction.IsPressed();

    }
}
