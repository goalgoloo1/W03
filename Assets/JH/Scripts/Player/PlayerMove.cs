using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    PlayerGround _playerGround;
    Rigidbody2D _rigid;
    float _velocityX;

    bool _onGround;

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

        // �̵� ����� ������ ������
        if (move != Vector2.zero)
        {
            // �ݴ� �������� �̵� ����� ������ turnSpeed ����
            if (Mathf.Sign(move.x) != Mathf.Sign(_velocityX))
            {
                _speedChange = _turnSpeed * Time.unscaledDeltaTime;
            } // ���� �������� �̵� ����� ������ ���ӵ� ����
            else
            {
                _speedChange = _acceleration * Time.unscaledDeltaTime;
            }
        } 
        else // �̵� ����� ������ ���� ������ ���ӵ� ����
        {
           _speedChange = _deceleration * Time.unscaledDeltaTime;
        }

        _velocityX = Mathf.MoveTowards(_velocityX, desiredVelocity.x, _speedChange);
        _rigid.linearVelocityX = _velocityX;
    }
}
