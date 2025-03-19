using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    Rigidbody2D _rigid;
    float _velocityX;

    // 아래는 조작감을 위해 조정해야할 변수
    [SerializeField] float _speed = 5f;
    [SerializeField] float _acceleration = 100f;
    [SerializeField] float _decceleration = 50f;
    [SerializeField] float _turnSpeed = 300f;

    // 아래는 계산시 사용되는 값을 건드리지 말아야할 변수
    float _speedChange;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
        _velocityX = _rigid.linearVelocity.x;
    }

    void Move()
    {
        Vector2 move = InputManager.Instance.Move;
        Vector3 desiredVelocity = new Vector3(move.x, 0, 0) * _speed;

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
           _speedChange = _decceleration * Time.unscaledDeltaTime;
        }

        _velocityX = Mathf.MoveTowards(_velocityX, desiredVelocity.x, _speedChange);
        _rigid.linearVelocityX = _velocityX;
    }
}
