using UnityEngine;

public class PlayerGround : MonoBehaviour
{
    PlayerDash _playerDash;

    Vector3 _colliderOffset = new Vector3(0.4f, 0);
    const int _groundLayer = 1 << 6;
    float _groundLength = 0.53f;
    public bool OnGround => _onGround;
    bool _onGround = true;

    void Start()
    {
        _playerDash = GetComponent<PlayerDash>();
    }

    void FixedUpdate()
    {
        CheckGround();
    }

    void CheckGround()
    {
        _onGround = Physics2D.Raycast(transform.position + _colliderOffset, Vector2.down, _groundLength, _groundLayer) || Physics2D.Raycast(transform.position - _colliderOffset, Vector2.down, _groundLength, _groundLayer);
        if (_onGround)
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
        if (_onGround) { Gizmos.color = Color.green; } else { Gizmos.color = Color.red; }
        Gizmos.DrawLine(transform.position + _colliderOffset, transform.position + _colliderOffset + Vector3.down * _groundLength);
        Gizmos.DrawLine(transform.position - _colliderOffset, transform.position - _colliderOffset + Vector3.down * _groundLength);
    }
}
