using UnityEngine;

public class PlayerWall : MonoBehaviour
{
    PlayerSprite _playerSprite;
    Vector3 _colliderOffset = new Vector3(0, 0.5f);
    const int _groundLayer = 1 << 6;
    public float WallLength => _wallLength;
    float _wallLength = 0.7f;
    public bool OnWall => _onWall;
    bool _onWall;
    public bool OnLeftWall => _onLeftWall;
    bool _onLeftWall;
    public bool OnRightWall => _onRightWall;
    bool _onRightWall;
    public bool OnHoldWall { get { return _onHoldWall; } 
        set 
        { 
            _onHoldWall = value;
            if (_onHoldWall)
            {
                _playerSprite.SetSprite(ESprite.Idle);
            }
        } 
    }
    bool _onHoldWall;

    private void Start()
    {
        _playerSprite = GetComponent<PlayerSprite>();
    }

    void Update()
    {
        CheckWall();
    }

    void CheckWall()
    {
        _onLeftWall = Physics2D.Raycast(transform.position, Vector2.left, _wallLength, _groundLayer) || Physics2D.Raycast(transform.position + _colliderOffset, Vector2.left, _wallLength, _groundLayer) || Physics2D.Raycast(transform.position - _colliderOffset, Vector2.left, _wallLength, _groundLayer);
        _onRightWall = Physics2D.Raycast(transform.position, Vector2.right, _wallLength, _groundLayer) || Physics2D.Raycast(transform.position + _colliderOffset, Vector2.right, _wallLength, _groundLayer) || Physics2D.Raycast(transform.position - _colliderOffset, Vector2.right, _wallLength, _groundLayer);
        _onWall = _onLeftWall || _onRightWall;
    }
    void OnDrawGizmos()
    {
        // 왼쪽 벽에 닿았는지 기즈모로 표현
        if (_onLeftWall) { Gizmos.color = Color.green; } else { Gizmos.color = Color.red; }
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * _wallLength);
        Gizmos.DrawLine(transform.position + _colliderOffset, transform.position + _colliderOffset + Vector3.left * _wallLength);
        Gizmos.DrawLine(transform.position - _colliderOffset, transform.position - _colliderOffset + Vector3.left * _wallLength);

        // 오른쪽 벽에 닿았는지 기즈모로 표현
        if (_onRightWall) { Gizmos.color = Color.green; } else { Gizmos.color = Color.red; }
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * _wallLength);
        Gizmos.DrawLine(transform.position + _colliderOffset, transform.position + _colliderOffset + Vector3.right * _wallLength);
        Gizmos.DrawLine(transform.position - _colliderOffset, transform.position - _colliderOffset + Vector3.right * _wallLength);
    }
}
