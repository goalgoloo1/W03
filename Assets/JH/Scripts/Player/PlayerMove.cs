using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    PlayerGround _playerGround;
    PlayerWall _playerWall;
    PlayerSprite _playerSprite;
    Rigidbody2D _rigid;

    float _velocityX;

    bool _onGround;
    public int Direction { get { return _direction; } set { _direction = value; } }
    int _direction = 1;
    Vector2 move;
    float desiredX;
    

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
        _playerWall = GetComponent<PlayerWall>();
        _rigid = GetComponent<Rigidbody2D>();
        _playerSprite = GetComponent<PlayerSprite>();
    }

    private void Update()
    {
        move = InputManager.Instance.Move;
        desiredX = move.x * _speed;
    }

    private void FixedUpdate()
    {
        _onGround = _playerGround.OnGround;
        Move();
    }

    void Move()
    {

        if (!InputManager.Instance.CanMove) return;
        if (_playerWall.OnHoldWall) return;

        //_velocityX = _rigid.linearVelocityX;

        //_acceleration = _onGround ? _groundAcceleration : _airAcceleration;
        //_deceleration = _onGround ? _groundDeceleration : _airDeceleration;
        //_turnSpeed = _onGround ? _groundTurnSpeed : _airTurnSpeed;

        //// 이동 명령을 내리고 있으면
        //if (move != Vector2.zero)
        //{
        //    // 반대 방향으로 이동 명령을 내리면 turnSpeed 적용
        //    if ((Mathf.Sign(move.x) != Mathf.Sign(_velocityX)) && (Mathf.Abs(_velocityX) > 0.2f))
        //    {
        //        print($"반대 방향 이동 명령 {move.x}, {_velocityX}");
        //        _speedChange = _turnSpeed * Time.deltaTime;
        //    }
        //    else // 같은 방향으로 이동 명령을 내리면 가속도 적용
        //    {
        //        _speedChange = _acceleration * Time.deltaTime;
        //    }
        //}
        //else // 이동 명령을 내리고 있지 않으면 감속도 적용
        //{
        //    _speedChange = _deceleration * Time.deltaTime;
        //}

        //_velocityX = Mathf.MoveTowards(_velocityX, desiredX, _speedChange);
        if (Mathf.Abs(desiredX) > Utility.SideInputThreshold)
        {
            Direction = (int)Mathf.Sign(desiredX);
            _playerSprite.SetSpriteFlipX(Direction == 1 ? false : true);
        }
        _velocityX = desiredX;
        _rigid.linearVelocityX = _velocityX;
    }
}
