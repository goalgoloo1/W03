using UnityEngine;

public class PlayerGround : MonoBehaviour
{
    PlayerDash _playerDash;
    PlayerSprite _playerSprite;
    PlayerJump _playerJump;

    Vector3 _colliderOffset = new Vector3(0.48f, 0);
    const int _groundLayer = 1 << 6;
    //float _groundLength = 0.53f;
    float _groundLength = 0.7f;
    public bool OnGround { get { return _onGround; } 
        set
        {
            _onGround = value;
            if (_onGround && !_playerJump.OnJump)
            {
                _playerSprite.SetSprite(ESprite.Idle);
            }
        } 
    } 
    bool _onGround = true;

    void Start()
    {
        _playerDash = GetComponent<PlayerDash>();
        _playerSprite = GetComponent<PlayerSprite>();
        _playerJump = GetComponent<PlayerJump>();
    }

    void FixedUpdate()
    {
        CheckGround();
    }

    void CheckGround()
    {
        OnGround = Physics2D.Raycast(transform.position + _colliderOffset, Vector2.down, _groundLength, _groundLayer) || Physics2D.Raycast(transform.position - _colliderOffset, Vector2.down, _groundLength, _groundLayer);
        if (OnGround)
        {
            // ������ �뽬�� �ϸ� �ٷ� HasDashed�� false �ż� �뽬�� �ι� �� �� ����.
            // �Ʒ� ���ǹ����� ���� �뽬���� �ƴ��� Ȯ��
            if (!_playerDash.OnDash)
            {
                _playerDash.HasDashed = false;
            }
            _playerDash.EndDash = false;
        }
    }

    void OnDrawGizmos()
    {
        // ���� ��Ҵ��� ������ ǥ��
        if (OnGround) { Gizmos.color = Color.green; } else { Gizmos.color = Color.red; }
        Gizmos.DrawLine(transform.position + _colliderOffset, transform.position + _colliderOffset + Vector3.down * _groundLength);
        Gizmos.DrawLine(transform.position - _colliderOffset, transform.position - _colliderOffset + Vector3.down * _groundLength);
    }
}
