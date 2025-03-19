using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance => _instance;
    static InputManager _instance;

    public Action OnJumpEvent;

    public Vector2 Move => _move;
    Vector2 _move;

    public bool Jump => _jump;
    bool _jump;

    private void Awake()
    {
        _instance = this;
    }

    void OnMove(InputValue value)
    {
        _move = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        //_jump = value.isPressed;
        //if (_jump)
        //{
            OnJumpEvent?.Invoke();
        //    _jump = false;
        //}
    }
}
