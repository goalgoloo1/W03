using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHoldWall : MonoBehaviour
{
    Rigidbody2D _rigid;
    PlayerWall _playerWall;
    PlayerDash _playerDash;
    Vector2 _velocity;

    [Header("벽")]
    [SerializeField] float _slideSpeed;
    [SerializeField] float _climbSpeed;
    [Tooltip("벽을 잡고 아래로 내려갈 때 더 빨라지게 하는 변수입니다.")] [SerializeField] float _climbDownSpeedModifier;

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _playerWall = GetComponent<PlayerWall>();
        _playerDash = GetComponent<PlayerDash>();
    }

    void FixedUpdate()
    {
        // 벽에 닿아 있지 않거나 점프 했을 때 아래의 이 스크립트의 Update 실행
        if (!_playerWall.OnWall || InputManager.Instance.Jump)
        {
            _playerWall.OnHoldWall = false;
            return;
        }

        if (InputManager.Instance.Hold)
        {
            Hold();
        }
        // 벽과 같은 방향으로 입력하고 있으면 벽에서 미끄러지기
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

        _velocity = _rigid.linearVelocity;

        // 벽을 잡았을 때 좌우로 점점 멀어지면서 떨어지는 버그 방지
        _velocity.x = 0;
        float _desireY = InputManager.Instance.Move.y;
        float _speedModifier = _desireY > 0 ? 1 : _climbDownSpeedModifier;
        _velocity.y = InputManager.Instance.Move.y * _climbSpeed * _speedModifier;

        // 벽면에 살짝 떨어져서 잡았을 때 붙게 만드는 함수
        //Ray ray = new Ray(transform.position, _playerWall.OnRightWall ? Vector2.right : Vector2.left);
        //if (Physics.Raycast(ray, _playerWall.WallLength))
        //{

        //}
        _rigid.linearVelocity = _velocity;
    }

    //public void HoldOff()
    //{
    //    _playerWall.OnHoldWall = false;
    //}

    //IEnumerator CoEndHold()
    //{

    //}
}
