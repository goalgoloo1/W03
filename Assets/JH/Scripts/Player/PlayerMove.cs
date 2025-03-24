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
    

    // �Ʒ��� ���۰��� ���� �����ؾ��� ����
    [Header("�ӵ�")]
    [SerializeField] float _speed;

    [Header("���� ������")]
    [SerializeField] float _groundAcceleration;
    [SerializeField] float _groundDeceleration;
    [SerializeField] float _groundTurnSpeed;

    [Header("���� ������")]
    [SerializeField] float _airAcceleration;
    [SerializeField] float _airDeceleration;
    [SerializeField] float _airTurnSpeed;

    // �Ʒ��� ���� ���Ǵ� ���� �ǵ帮�� ���ƾ��� ����
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

        //// �̵� ����� ������ ������
        //if (move != Vector2.zero)
        //{
        //    // �ݴ� �������� �̵� ����� ������ turnSpeed ����
        //    if ((Mathf.Sign(move.x) != Mathf.Sign(_velocityX)) && (Mathf.Abs(_velocityX) > 0.2f))
        //    {
        //        print($"�ݴ� ���� �̵� ��� {move.x}, {_velocityX}");
        //        _speedChange = _turnSpeed * Time.deltaTime;
        //    }
        //    else // ���� �������� �̵� ����� ������ ���ӵ� ����
        //    {
        //        _speedChange = _acceleration * Time.deltaTime;
        //    }
        //}
        //else // �̵� ����� ������ ���� ������ ���ӵ� ����
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
