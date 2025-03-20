using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    PlayerGround _playerGround;
    Rigidbody2D _rigid;
    float _velocityY;

    bool _onGround;

    // 아래는 조작감을 위해 조정해야할 변수
    [Header("점프")]
    [SerializeField] float _jumpHeight;
    [SerializeField] float _timeToJumpApex;   // 최고 높은 곳으로 가기까지 걸리는 시간??? 공부필요함
    [SerializeField] float _upMoveMultiplier;
    [SerializeField] float _downMoveMultiplier;

    // 아래는 계산시 사용되는 값을 건드리지 말아야할 변수
    float _jumpSpeed;
    float _gravityMultiplier;
    float _defaultGravityScale = 1; // 예제는 Awake에서 선언했는데 그냥 선언해줘도 될듯?

    void Start()
    {
        _playerGround = GetComponent<PlayerGround>();
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

    // 버튼을 누를 때 점프하는 행동
    void Jump()
    {
        if (!_onGround || !InputManager.Instance.Jump) return;

        ///
        /// 이부분 코드 맞는지 검토 필요
        ///
        _velocityY = _rigid.linearVelocityY;
        _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _rigid.gravityScale * _jumpHeight);
        // 올라가는 탈것 등에 있어서 y축으로 올라가고 있다면 그것을 뺀 만큼만 점프
        if (_velocityY > 0f)
        {
            _jumpSpeed = Mathf.Max(_jumpSpeed - _velocityY, 0f);
        }
        // 내려가는 탈것 등에 있어서 y축으로 내려가고 있다면 그것을 뺀 만큼만 점프
        else if (_velocityY < 0f)
        {
            _jumpSpeed += Mathf.Abs(_rigid.linearVelocityY);
        }

        _velocityY += _jumpSpeed;
        _rigid.linearVelocityY = _velocityY;

        InputManager.Instance.Jump = false;
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
            _gravityMultiplier = _defaultGravityScale;
        }
    }
    
    void SetPhysics()
    {
        float newGravity = -2 * (_jumpHeight) / (_timeToJumpApex * _timeToJumpApex);
        _rigid.gravityScale = newGravity / Physics2D.gravity.y * _gravityMultiplier;
    }
}
