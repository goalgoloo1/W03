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
            // 땅에서 대쉬를 하면 바로 HasDashed가 false 돼서 대쉬를 두번 할 수 있음.
            // 아래 조건문으로 지금 대쉬중이 아님을 확인
            if (!_playerDash.OnDash)
            {
                _playerDash.HasDashed = false;
            }
            _playerDash.EndDash = false;
        }
    }

    void OnDrawGizmos()
    {
        // 땅에 닿았는지 기즈모로 표현
        if (OnGround) { Gizmos.color = Color.green; } else { Gizmos.color = Color.red; }
        Gizmos.DrawLine(transform.position + _colliderOffset, transform.position + _colliderOffset + Vector3.down * _groundLength);
        Gizmos.DrawLine(transform.position - _colliderOffset, transform.position - _colliderOffset + Vector3.down * _groundLength);
    }
}
