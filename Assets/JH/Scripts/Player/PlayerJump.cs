using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    PlayerGround _playerGround;
    Rigidbody2D _rigid;
    float _velocityY;

    bool _onGround;

    // �Ʒ��� ���۰��� ���� �����ؾ��� ����
    [Header("����")]
    [SerializeField] float _jumpHeight;
    [SerializeField] float _timeToJumpApex;   // �ְ� ���� ������ ������� �ɸ��� �ð�??? �����ʿ���
    [SerializeField] float _upMoveMultiplier;
    [SerializeField] float _downMoveMultiplier;

    // �Ʒ��� ���� ���Ǵ� ���� �ǵ帮�� ���ƾ��� ����
    float _jumpSpeed;
    float _gravityMultiplier;
    float _defaultGravityScale = 1; // ������ Awake���� �����ߴµ� �׳� �������൵ �ɵ�?

    void Start()
    {
        _playerGround = GetComponent<PlayerGround>();
        _rigid = GetComponent<Rigidbody2D>();
        InputManager.Instance.OnJumpEvent += Jump;
    }
    void Update()
    {
        _onGround = _playerGround.OnGround;
    }

    void FixedUpdate()
    {
        CalculateGravity();
        SetPhysics();
        Jump();
    }

    // ��ư�� ���� �� �����ϴ� �ൿ
    void Jump()
    {
        if (!_onGround || !InputManager.Instance.Jump) return;

        ///
        /// �̺κ� �ڵ� �´��� ���� �ʿ�
        ///
        _velocityY = _rigid.linearVelocityY;
        _jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * _rigid.gravityScale * _jumpHeight);
        // �ö󰡴� Ż�� � �־ y������ �ö󰡰� �ִٸ� �װ��� �� ��ŭ�� ����
        if (_velocityY > 0f)
        {
            _jumpSpeed = Mathf.Max(_jumpSpeed - _velocityY, 0f);
        }
        // �������� Ż�� � �־ y������ �������� �ִٸ� �װ��� �� ��ŭ�� ����
        else if (_velocityY < 0f)
        {
            _jumpSpeed += Mathf.Abs(_rigid.linearVelocityY);
        }

        _velocityY += _jumpSpeed;
        _rigid.linearVelocityY = _velocityY;

        InputManager.Instance.Jump = false;
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
