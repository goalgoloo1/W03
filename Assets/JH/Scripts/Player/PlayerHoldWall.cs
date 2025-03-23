using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHoldWall : MonoBehaviour
{
    Rigidbody2D _rigid;
    PlayerWall _playerWall;
    PlayerDash _playerDash;
    float _velocityY;

    [Header("��")]
    [SerializeField] float _slideSpeed;
    [SerializeField] float _climbSpeed;
    [Tooltip("���� ��� �Ʒ��� ������ �� �� �������� �ϴ� �����Դϴ�.")] [SerializeField] float _climbDownSpeedModifier;

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playerWall = GetComponent<PlayerWall>();
        _playerDash = GetComponent<PlayerDash>();
    }

    void FixedUpdate()
    {
        // ���� ��� ���� �ʰų� ���� ���� �� �Ʒ��� �� ��ũ��Ʈ�� Update ����
        if (!_playerWall.OnWall || InputManager.Instance.Jump)
        {
            _playerWall.OnHoldWall = false;
            return;
        }

        

        if (InputManager.Instance.Hold)
        {
            Hold();
        }
        // ���� ���� �������� �Է��ϰ� ������ ������ �̲�������
        else
        {
            _playerWall.OnHoldWall = false;
            //    if ((_playerWall.OnLeftWall && InputManager.Instance.Move.x < -Utility.SideInputThreshold )|| (_playerWall.OnRightWall && InputManager.Instance.Move.x > Utility.SideInputThreshold))
            //    {
            //        WallSlide();
            //    }
        }
    }

    void WallSlide()
    {
        //_velocity = _rigid.linearVelocity;
        //_velocity.y = -_slideSpeed;
        //_rigid.linearVelocity = _velocity;
    }

    void Hold()
    {
        if (!InputManager.Instance.CanMove) return;
        _playerDash.EndDash = false;
        _playerWall.OnHoldWall = true;

        _velocityY = _rigid.linearVelocityY;
        float _desireY = InputManager.Instance.Move.y;
        float _speedModifier = _desireY > 0 ? 1 : _climbDownSpeedModifier;
        _velocityY = InputManager.Instance.Move.y * _climbSpeed * _speedModifier;

        // ���鿡 ��¦ �������� ����� �� �ٰ� ����� �Լ�
        //Ray ray = new Ray(transform.position, _playerWall.OnRightWall ? Vector2.right : Vector2.left);
        //if (Physics.Raycast(ray, _playerWall.WallLength))
        //{

        //}
        _rigid.linearVelocityY = _velocityY;
        
    }
}
