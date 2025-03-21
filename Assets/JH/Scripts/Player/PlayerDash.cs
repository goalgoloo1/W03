using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    Rigidbody2D _rigid;
    Vector2 _velocity;

    public bool HasDashed { get { return _hasDashed; } set { _hasDashed = value; } }
    bool _hasDashed;
    public bool OnDash { get {  return _onDash; } set { _onDash = value; } }
    bool _onDash;

    [SerializeField] float _dashSpeed;
    [SerializeField] float _dashCoolDown;

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        Dash();
    }

    void Dash()
    {
        if (!InputManager.Instance.Dash) return;
        InputManager.Instance.Dash = false;
        if (!InputManager.Instance.CanMove || _hasDashed) return;

        Debug.Log("Dash!");
        _hasDashed = true;
        StartCoroutine(CoDashWait());
        _rigid.linearVelocity = Vector2.zero;
        _rigid.linearVelocity += InputManager.Instance.Move.normalized * _dashSpeed;
    }

    IEnumerator CoDashWait()
    {
        OnDash = true;
        InputManager.Instance.CanMove = false;
        yield return new WaitForSeconds(_dashCoolDown);
        OnDash = false;
        InputManager.Instance.CanMove = true;
    }
}

