using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    PlayerGround _playerGround;
    PlayerWall _playerWall;
    PlayerDash _playerDash;
    Rigidbody2D _rigid;
    Vector2 _velocity;

    bool _onGround;

    // �Ʒ��� ���۰��� ���� �����ؾ��� ����
    [Header("����")]
    [SerializeField] float _jumpHeight;
    [SerializeField] float _timeToJumpApex;   // �ְ� ���� ������ ������� �ɸ��� �ð�??? �����ʿ���
    [SerializeField] float _upMoveMultiplier;
    [SerializeField] float _downMoveMultiplier;

    [Header("�� ����")]
    [SerializeField] float _wallJumpXSpeed;
    [SerializeField] float _wallJumpMultiplier;
    [SerializeField] float _wallStillJumpMultiplier;
    [SerializeField] float _wallJumpCannotMoveDuration;

    [Header("�뽬")]
    [Tooltip("�뽬 �� ���� ��ų� ���� ���� �� ���� ���� �߷� ��")] [SerializeField] float _endDashGravity;

    [Header("��Ÿ")]
    [SerializeField] float _speedLimit;


    // �Ʒ��� ���� ���Ǵ� ���� �ǵ帮�� ���ƾ��� ����
    float _jumpSpeed;
    float _gravityMultiplier;
    float _defaultGravityScale = 1; // ������ Awake���� �����ߴµ� �׳� �������൵ �ɵ�?

    void Start()
    {
        _playerGround = GetComponent<PlayerGround>();
        _playerWall = GetComponent<PlayerWall>();
        _playerDash = GetComponent<PlayerDash>();
        _rigid = GetComponent<Rigidbody2D>();
        InputManager.Instance.OnJumpEvent += Jump;
    }
    void Update()
    {
        _onGround = _playerGround.OnGround;
    }

    void FixedUpdate()
    {
        CalculateGravity();
        SetPhysics();
        Jump();
    }

    Coroutine _coDelayJumpfalse;

    // ��ư�� ���� �� �����ϴ� �ൿ
    void Jump()
    {
        // (���� ���� �ʰ� ������ �پ����� �ʴ� )���߿� ���ִ� ��� �Ǵ� ������ ������ �ʾҴ� ���
        if ((!_onGround && !_playerWall.OnWall) || !InputManager.Instance.Jump)
        {
            //InputManager.Instance.Jump = false;
            if (_coDelayJumpfalse == null)
            {
                _coDelayJumpfalse = StartCoroutine(CoDelayJumpFalse());
            }
            
            return;
        }

        if (!InputManager.Instance.CanMove) return;

        if (_playerWall.OnWall && !_onGround)
        {
            WallJump();
            return;
        }

        ///
        /// �̺κ� �ڵ� �´��� ���� �ʿ�
        ///
        _velocity = _rigid.linearVelocity;
        _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _rigid.gravityScale * _jumpHeight);
        // �ö󰡴� Ż�� � �־ y������ �ö󰡰� �ִٸ� �װ��� �� ��ŭ�� ����
        if (_velocity.y > 0f)
        {
            _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y, 0f);
        }
        // �������� Ż�� � �־ y������ �������� �ִٸ� �װ��� �� ��ŭ�� ����
        else if (_velocity.y < 0f)
        {
            _jumpSpeed += Mathf.Abs(_rigid.linearVelocityY);
        }

        _velocity.y += _jumpSpeed;
        _rigid.linearVelocityY = _velocity.y;

        InputManager.Instance.Jump = false;
    }
    
    void WallJump()
    {
        ///
        /// �̺κ� �ڵ� �´��� ���� �ʿ�
        ///
        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(_wallJumpCannotMoveDuration));

        _velocity = _rigid.linearVelocity;

        // ���� ���� �ݴ������� �����Ϸ��� �� �� Velocity�� X�� ����
        float side = 0;
        
        //Debug.Log(InputManager.Instance.Move.x);
        if (_playerWall.OnLeftWall)
        {
            if (InputManager.Instance.Move.x > Utility.SideInputThreshold)
            {
                side = 1;
            }
            else
            {

                side = _wallStillJumpMultiplier;
            }
        }
        else if (_playerWall.OnRightWall)
        {
            if (InputManager.Instance.Move.x < -Utility.SideInputThreshold)
            {
                side = -1;
            }
            else
            {
                side = -_wallStillJumpMultiplier;
            }
        }
        else
        {
            Debug.LogError("Wall Jump�ε� Left Wall, Right Wall ��� False");
        }

        // Velocity�� Y�� ����
        _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _rigid.gravityScale * _jumpHeight);
        // �ö󰡴� Ż�� � �־ y������ �ö󰡰� �ִٸ� �װ��� �� ��ŭ�� ����
        if (_velocity.y > 0f)
        {
            _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y, 0f);
        }
        // �������� Ż�� � �־ y������ �������� �ִٸ� �װ��� �� ��ŭ�� ����
        else if (_velocity.y < 0f)
        {
            _jumpSpeed += Mathf.Abs(_rigid.linearVelocityY);
        }

         _velocity.x += _wallJumpXSpeed * side;
        _velocity.y += _jumpSpeed;
        _rigid.linearVelocity = _velocity * _wallJumpMultiplier;

        InputManager.Instance.Jump = false;
    }

    IEnumerator CoDelayJumpFalse()
    {
        yield return new WaitForSeconds(0.2f);
        InputManager.Instance.Jump = false;
        _coDelayJumpfalse = null;
    }

    IEnumerator DisableMovement(float time)
    {
        InputManager.Instance.CanMove = false;
        yield return new WaitForSeconds(time);
        InputManager.Instance.CanMove = true;
    }

    // ĳ������ Y�࿡ ���� �߷� ����
    void CalculateGravity()
    {
        // ��� �߰��� ��츦 ����� ����ϴ� ���� �ϰ��ϴ� ���� ���� ����
        // ����ϴ� ��� ���� �ִ� ��찡 �ƴ϶�� ��°�� ������
        if (_rigid.linearVelocityY > 0.01f)
        {
            if (_onGround)
            {
                _gravityMultiplier = _defaultGravityScale;
            }
            else
            {
                _gravityMultiplier = _upMoveMultiplier;
            }
        }
        // �ϰ��ϴ� ��� ���� �ִ� ��찡 �ƴ϶�� �ϰ���� ������
        else if (_rigid.linearVelocityY < -0.01f)
        {
            if (_onGround)
            {
                _gravityMultiplier = _defaultGravityScale;
            }
            else
            {
                _gravityMultiplier = _downMoveMultiplier;
            }
        }
        else
        {
            _gravityMultiplier = _defaultGravityScale;
        }
        // y���� �ӵ��� �ʹ� �����ų� ������ �ʰ� ����
        if (Mathf.Abs(_rigid.linearVelocityY) > 30)
            print(_rigid.linearVelocityY);
        _rigid.linearVelocityY = Mathf.Clamp(_rigid.linearVelocityY, -_speedLimit, _speedLimit);
    }
    
    void SetPhysics()
    {
        // �÷��̾ ���߿��� ���� ��� ���� �� �߷� 0 �Ǵ� �뽬���� �� �߷� 0 
        if ((_playerWall.OnHoldWall && !_playerGround.OnGround) || _playerDash.OnDash)
        {
            _rigid.gravityScale = 0;
            return;
        }
        float newGravity = -2 * (_jumpHeight) / (_timeToJumpApex * _timeToJumpApex);

        // Dash�� �������� �ϴ� ��� �ʹ� ���̱��� ���� �����. _endDashGravity�� ���� ��ų� ���� ���� �� ���� �߷��� ����
        if (_playerDash.EndDash)
        {
            _rigid.gravityScale = _endDashGravity;
        }
        else
        {
            _rigid.gravityScale = newGravity / Physics2D.gravity.y * _gravityMultiplier;
        }
    }
}
