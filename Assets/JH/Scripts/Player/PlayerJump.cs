using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    Rigidbody2D _rigid;
    float _velocityY;

    Vector3 _colliderOffset = new Vector3(0.5f, 0);
    int _groundLayer = 6;
    float _groundLength = 0.53f;
    bool _onGround = true;

    // �Ʒ��� ���۰��� ���� �����ؾ��� ����
    [SerializeField] float _jumpHeight = 5f;
    [SerializeField] float _timeToJumpApex = 1f;   // �ְ� ���� ������ ������� �ɸ��� �ð�??? �����ʿ���
    [SerializeField] float _upMoveMultiplier = 1f;
    [SerializeField] float _downMoveMultiplier = 5f;

    // �Ʒ��� ���� ���Ǵ� ���� �ǵ帮�� ���ƾ��� ����
    float _jumpSpeed;
    float _gravityMultiplier;
    float _defaultGravityScale = 1; // ������ Awake���� �����ߴµ� �׳� �������൵ �ɵ�?

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

    // ��ư�� ���� �� �����ϴ� �ൿ
    void Jump()
    {
        if (!_onGround) return;

        ///
        /// �̺κ� �ڵ� �´��� ���� �ʿ�
        ///
        _velocityY = _rigid.linearVelocity.y;
        _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _rigid.gravityScale * _jumpHeight);
        // �ö󰡴� Ż�� � �־ y������ �ö󰡰� �ִٸ� �װ��� �� ��ŭ�� ����
        if (_velocityY > 0f)
        {
            _jumpSpeed = Mathf.Max(_jumpSpeed - _velocityY, 0f);
        }
        // �������� Ż�� � �־ y������ �������� �ִٸ� �װ��� �� ��ŭ�� ����
        else if (_velocityY < 0f)
        {
            _jumpSpeed += Mathf.Abs(_rigid.linearVelocity.y);
        }

        _velocityY += _jumpSpeed;
        _rigid.linearVelocityY = _velocityY;
    }

    // ĳ������ Y�࿡ ���� �߷� ����
    void CalculateGravity()
    {
        // ��� �߰��� ��츦 ����� ����ϴ� ���� �ϰ��ϴ� ���� ���� ����
        // ����ϴ� ��� ���� �ִ� ��찡 �ƴ϶�� ��°�� ������
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
        // �ϰ��ϴ� ��� ���� �ִ� ��찡 �ƴ϶�� �ϰ���� ������
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
