using System.Collections;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    Rigidbody2D _rigid;
    Vector2 _velocity;
    PlayerMove _playerMove;

    public bool HasDashed { get { return _hasDashed; } set { _hasDashed = value; } } // Dash�� �̹� �ؼ� Dash�� �� �� �� ������
    bool _hasDashed;
    public bool OnDash { get {  return _onDash; } set { _onDash = value; } } // Dash�� ���� �ϰ� �ִ���
    bool _onDash;
    public bool EndDash { get { return _endDash; } set { _endDash = value; } } // Dash�� ���� �������� (�߷� �����ֱ� ��)
    bool _endDash;

    [SerializeField] float _dashSpeed;
    [SerializeField] float _dashCoolDown;

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playerMove = GetComponent<PlayerMove>();
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

        HasDashed = true;
        OnDash = true;
        StartCoroutine(CoDashWait());
        _rigid.linearVelocity = Vector2.zero;

        // input ������ �� �ſ� ������ ���������� �ٶ� �¿� �������� �뽬 ���
        if (InputManager.Instance.Move.magnitude < Utility.InputMagitudeThreshold)
        {
            _rigid.linearVelocity += new Vector2(_playerMove.Direction, 0) * _dashSpeed;
        }
        // input �������� ũ�� �� �������� �뽬
        else
        {
            _rigid.linearVelocity += InputManager.Instance.Move.normalized * _dashSpeed;
        }
    }

    IEnumerator CoDashWait()
    {
        InputManager.Instance.CanMove = false;
        yield return new WaitForSeconds(_dashCoolDown);
        OnDash = false;
        InputManager.Instance.CanMove = true;
        _endDash = true;
    }
}

