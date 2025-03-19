using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    Rigidbody2D _rigid;
    float _velocityY;

    Vector3 _colliderOffset = new Vector3(0.5f, 0);
    int _groundLayer = 6;
    float _groundLength = 0.53f;
    bool _onGround = true;

    // 아래는 조작감을 위해 조정해야할 변수
    [SerializeField] float _jumpHeight = 5f;
    [SerializeField] float _timeToJumpApex = 1f;   // 최고 높은 곳으로 가기까지 걸리는 시간??? 공부필요함
    [SerializeField] float _upMoveMultiplier = 1f;
    [SerializeField] float _downMoveMultiplier = 5f;

    // 아래는 계산시 사용되는 값을 건드리지 말아야할 변수
    float _jumpSpeed;
    float _gravityMultiplier;
    float _defaultGravityScale = 1; // 예제는 Awake에서 선언했는데 그냥 선언해줘도 될듯?

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();

        InputManager.Instance.OnJumpEvent += Jump;
    }
    void Update()
    {
        CheckGround();
        SetPhysics();
    }

    void FixedUpdate()
    {
        CalculateGravity();
    }

    void CheckGround()
    {
        _onGround = Physics2D.Raycast(transform.position + _colliderOffset, Vector2.down, _groundLength) || Physics2D.Raycast(transform.position - _colliderOffset, Vector2.down, _groundLength);
    }

    private void OnDrawGizmos()
    {
        //Draw the ground colliders on screen for debug purposes
        if (_onGround) { Gizmos.color = Color.green; } else { Gizmos.color = Color.red; }
        Gizmos.DrawLine(transform.position + _colliderOffset, transform.position + _colliderOffset + Vector3.down * _groundLength);
        Gizmos.DrawLine(transform.position - _colliderOffset, transform.position - _colliderOffset + Vector3.down * _groundLength);
    }

    // 버튼을 누를 때 점프하는 행동
    void Jump()
    {
        if (!_onGround) return;

        ///
        /// 이부분 코드 맞는지 검토 필요
        ///
        _velocityY = _rigid.linearVelocity.y;
        _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _rigid.gravityScale * _jumpHeight);
        // 올라가는 탈것 등에 있어서 y축으로 올라가고 있다면 그것을 뺀 만큼만 점프
        if (_velocityY > 0f)
        {
            _jumpSpeed = Mathf.Max(_jumpSpeed - _velocityY, 0f);
        }
        // 내려가는 탈것 등에 있어서 y축으로 내려가고 있다면 그것을 뺀 만큼만 점프
        else if (_velocityY < 0f)
        {
            _jumpSpeed += Mathf.Abs(_rigid.linearVelocity.y);
        }

        _velocityY += _jumpSpeed;
        _rigid.linearVelocityY = _velocityY;
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
