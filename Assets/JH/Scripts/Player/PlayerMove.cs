using UnityEngine;

[RequireComponent (typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    Rigidbody2D _rigid;
    float velocityX;

    [SerializeField] float _speed = 5f;
    [SerializeField] float _acceleration = 100f;
    [SerializeField] float _decceleration = 50f;
    [SerializeField] float _turnSpeed = 300f;
    float _speedChange;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
        velocityX = _rigid.linearVelocity.x;
    }

    void Move()
    {
        Vector2 move = InputManager.Instance.Move;
        Vector3 desiredVelocity = new Vector3(move.x, 0, 0) * _speed;

        // �̵� ����� ������ ������
        if (move != Vector2.zero)
        {
            // �ݴ� �������� �̵� ����� ������ turnSpeed ����
            if (Mathf.Sign(move.x) != Mathf.Sign(velocityX))
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
           _speedChange = _decceleration * Time.unscaledDeltaTime;
        }

        velocityX = Mathf.MoveTowards(velocityX, desiredVelocity.x, _speedChange);
        _rigid.linearVelocityX = velocityX;
    }
}
