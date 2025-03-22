using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance => _instance;
    static InputManager _instance;

    public Action OnJumpEvent;

    public Vector2 Move => _move;
    Vector2 _move;

    public bool Jump { get { return _jump; } set { _jump = value; } }
    bool _jump;

    public bool Hold => _hold;
    bool _hold;

    public bool CanMove { get { return _canMove; } set { _canMove = value; } }
    bool _canMove = true;

    public bool Dash { get { return _dash; } set { _dash = value; } }
    bool _dash;

    private void Awake()
    {
        _instance = this;
    }

    public void ActivateMovement(bool canMove)
    {
        _canMove = canMove;
    }

    void OnMove(InputValue value)
    {
        _move = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        _jump = value.isPressed;

    }

    void OnHold(InputValue value)
    {
        _hold = value.isPressed;
    }

    void OnDash(InputValue value)
    {
        _dash = value.isPressed;
    }
}
