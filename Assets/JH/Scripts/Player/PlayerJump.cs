using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    PlayerGround _playerGround;
    PlayerWall _playerWall;
    PlayerDash _playerDash;
    PlayerSprite _playerSprite;
    Rigidbody2D _rigid;
    Vector2 _velocity;

    bool _onGround;
    public bool OnJump { get { return _onJump; } 
        set 
        { 
            _onJump = value;
        }
    }
    bool _onJump;

    // 아래는 조작감을 위해 조정해야할 변수
    [Header("점프")]
    [SerializeField] float _jumpHeight;
    [SerializeField] float _timeToJumpApex;   // 최고 높은 곳으로 가기까지 걸리는 시간??? 공부필요함
    [SerializeField] float _upMoveMultiplier;
    [SerializeField] float _downMoveMultiplier;
    [SerializeField] float _coyoteTime;

    [Header("벽 점프")]
    [SerializeField] float _wallJumpHeight;
    [SerializeField] float _wallJumpXSpeed;
    [SerializeField] float _wallStillJumpMultiplier;
    [SerializeField] float _wallJumpCannotMoveDuration;

    [Header("대쉬")]
    [Tooltip("대쉬 후 땅에 닿거나 벽을 잡을 때 까지 높일 중력 값")] [SerializeField] float _endDashGravity;

    [Header("기타")]
    [SerializeField] float _speedLimit;


    // 아래는 계산시 사용되는 값을 건드리지 말아야할 변수
    float _jumpSpeed;
    float _gravityMultiplier;
    float _defaultGravityScale = 1; // 예제는 Awake에서 선언했는데 그냥 선언해줘도 될듯?
    float _coyoteTimeCounter = 0;

    Coroutine _coDelayJumpfalse;

    void Start()
    {
        _playerGround = GetComponent<PlayerGround>();
        _playerWall = GetComponent<PlayerWall>();
        _playerDash = GetComponent<PlayerDash>();
        _rigid = GetComponent<Rigidbody2D>();
        _playerSprite = GetComponent<PlayerSprite>();
        InputManager.Instance.OnJumpEvent += Jump;
    }
    void Update()
    {
        _onGround = _playerGround.OnGround;
        if (!OnJump && !_onGround && !_playerWall.OnWall && !_playerDash.HasDashed)
        {
            _coyoteTimeCounter += Time.deltaTime;
        } else
        {
            _coyoteTimeCounter = 0;
        }
    }

    void FixedUpdate()
    {
        CalculateGravity();
        SetPhysics();
        Jump();
    }


    // 버튼을 누를 때 점프하는 행동
    void Jump()
    {
        // [(땅에 있지 않고 벽에도 붙어있지 않는)공중에 떠있는 경우 이면서 코요테 점프도 해당되지 않는 경우] 또는 점프를 누르지 않았는 경우 return 으로 종료
        if ((!_onGround && !_playerWall.OnWall && (_coyoteTimeCounter < 0.03f || _coyoteTimeCounter > _coyoteTime)) || !InputManager.Instance.Jump || _playerDash.OnDash)
        {
            //InputManager.Instance.Jump = false;
            if (_coDelayJumpfalse == null)
            {
                _coDelayJumpfalse = StartCoroutine(CoDelayJumpFalse());
            }
            
            return;
        }

        if (!InputManager.Instance.CanMove) return;

        _coyoteTimeCounter = 0;
        // 점프하는 스프라이트로 전환
        _playerSprite.SetSprite(ESprite.Jump);

        // 벽에 있고 땅에 닿아있지 않으며 코요테 점프가 아닌 경우 벽점프
        if (_playerWall.OnWall && !_onGround && !(_coyoteTimeCounter > 0.03f && _coyoteTimeCounter < _coyoteTime) )
        {
            WallJump();
            return;
        }

        ///
        /// 이부분 코드 맞는지 검토 필요
        ///
        _velocity = _rigid.linearVelocity;
        _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _rigid.gravityScale * _jumpHeight);
        // 올라가는 탈것 등에 있어서 y축으로 올라가고 있다면 그것을 뺀 만큼만 점프
        if (_velocity.y > 0f)
        {
            _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y, 0f);
        }
        // 내려가는 탈것 등에 있어서 y축으로 내려가고 있다면 그것을 뺀 만큼만 점프
        else if (_velocity.y < 0f)
        {
            _jumpSpeed += Mathf.Abs(_rigid.linearVelocityY);
        }

        _velocity.y += _jumpSpeed;
        _rigid.linearVelocityY = _velocity.y;

        OnJump = true;
        InputManager.Instance.Jump = false;
    }
    
    void WallJump()
    {
        ///
        /// 이부분 코드 맞는지 검토 필요
        ///
        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(_wallJumpCannotMoveDuration));

        _playerWall.OnHoldWall = false;
        _rigid.gravityScale = 0;

        _velocity = _rigid.linearVelocity;

        // 붙은 벽의 반대편으로 점프하려고 할 시 Velocity의 X값 조정
        float side = 0;

        //Debug.Log(InputManager.Instance.Move.x);
        //if (_playerWall.OnLeftWall)
        //{
        //    if (InputManager.Instance.Move.x > Utility.SideInputThreshold)
        //    {
        //        side = 1;
        //    }
        //    else
        //    {

        //        side = _wallStillJumpMultiplier;
        //    }
        //}
        //else if (_playerWall.OnRightWall)
        //{
        //    if (InputManager.Instance.Move.x < -Utility.SideInputThreshold)
        //    {
        //        side = -1;
        //    }
        //    else
        //    {
        //        side = -_wallStillJumpMultiplier;
        //    }
        //}
        //else
        //{
        //    Debug.LogError("Wall Jump인데 Left Wall, Right Wall 모두 False");
        //}
        if (_playerWall.OnLeftWall)
        {
            side = 1;
        }
        else if (_playerWall.OnRightWall)
        {
            side = -1;
        }
        else
        {
            Debug.LogError("Wall Jump인데 Left Wall, Right Wall 모두 False");
        }

        // Velocity의 Y값 조정
        //_jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _rigid.gravityScale * _wallJumpHeight);
        _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y  * _wallJumpHeight);
        // 올라가는 탈것 등에 있어서 y축으로 올라가고 있다면 그것을 뺀 만큼만 점프
        if (_velocity.y > 0f)
        {
            _jumpSpeed = Mathf.Max(_jumpSpeed - _velocity.y, 0f);
            print(_velocity.y);
        }
        // 내려가는 탈것 등에 있어서 y축으로 내려가고 있다면 그것을 뺀 만큼만 점프
        //else if (_velocity.y < 0f)
        //{
        //    _jumpSpeed += Mathf.Abs(_rigid.linearVelocityY);
        //}

        _velocity.y += _jumpSpeed;
        _velocity.x += _wallJumpXSpeed * side;
        _rigid.linearVelocity = _velocity;

        OnJump = true;
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

    // 캐릭터의 Y축에 따라 중력 조절
    void CalculateGravity()
    {
        // 기능 추가할 경우를 대비해 상승하는 경우와 하강하는 경우로 먼저 나눔
        // 상승하는 경우 땅에 있는 경우가 아니라면 상승계수 곱해줌
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
        // 하강하는 경우 땅에 있는 경우가 아니라면 하강계수 곱해줌
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
            if (_onGround)
            {
                OnJump = false;
            }
            _gravityMultiplier = _defaultGravityScale;
        }
        // y축의 속도가 너무 빠르거나 느리지 않게 제한
        //if (Mathf.Abs(_rigid.linearVelocityY) > 30)
            //print(_rigid.linearVelocityY);
        _rigid.linearVelocityY = Mathf.Clamp(_rigid.linearVelocityY, -_speedLimit, _speedLimit);
    }
    
    void SetPhysics()
    {
        // 플레이어가 공중에서 벽을 잡고 있을 때 중력 0 또는 대쉬중일 때 중력 0 
        if ((_playerWall.OnHoldWall && !_playerGround.OnGround) || _playerDash.OnDash)
        {
            _rigid.gravityScale = 0;
            return;
        }
        float newGravity = -2 * (_jumpHeight) / (_timeToJumpApex * _timeToJumpApex);

        // Dash를 공중으로 하는 경우 너무 높이까지 가서 어색함. _endDashGravity로 땅에 닿거나 벽에 닿을 때 까지 중력을 높임
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
