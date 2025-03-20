using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    PlayerGround _playerGround;
    Rigidbody2D _rigid;
    float _velocityX;

    bool _onGround;

    // 아래는 조작감을 위해 조정해야할 변수
    [Header("속도")]
    [SerializeField] float _speed;

    [Header("지상 움직임")]
    [SerializeField] float _groundAcceleration;
    [SerializeField] float _groundDeceleration;
    [SerializeField] float _groundTurnSpeed;

    [Header("공중 움직임")]
    [SerializeField] float _airAcceleration;
    [SerializeField] float _airDeceleration;
    [SerializeField] float _airTurnSpeed;




    // 아래는 계산시 사용되는 값을 건드리지 말아야할 변수
    float _acceleration;
    float _deceleration;
    float _turnSpeed;
    float _speedChange;

    private void Start()
    {
        _playerGround = GetComponent<PlayerGround>();
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _onGround = _playerGround.Ground;
        Move();
        _velocityX = _rigid.linearVelocity.x;
    }

    void Move()
    {
        Vector2 move = InputManager.Instance.Move;
        Vector3 desiredVelocity = new Vector3(move.x, 0, 0) * _speed;

        _acceleration = _onGround ? _groundAcceleration : _airAcceleration;
        _deceleration = _onGround ? _groundDeceleration : _airDeceleration;
        _turnSpeed = _onGround ? _groundTurnSpeed : _airTurnSpeed;

        // 이동 명령을 내리고 있으면
        if (move != Vector2.zero)
        {
            // 반대 방향으로 이동 명령을 내리면 turnSpeed 적용
            if (Mathf.Sign(move.x) != Mathf.Sign(_velocityX))
            {
                _speedChange = _turnSpeed * Time.unscaledDeltaTime;
            } // 같은 방향으로 이동 명령을 내리면 가속도 적용
            else
            {
                _speedChange = _acceleration * Time.unscaledDeltaTime;
            }
        } 
        else // 이동 명령을 내리고 있지 않으면 감속도 적용
        {
           _speedChange = _deceleration * Time.unscaledDeltaTime;
        }

        _velocityX = Mathf.MoveTowards(_velocityX, desiredVelocity.x, _speedChange);
        _rigid.linearVelocityX = _velocityX;
    }
}
