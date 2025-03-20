using UnityEngine;

public class PlayerGround : MonoBehaviour
{
    Vector3 _colliderOffset = new Vector3(0.5f, 0);
    //const int _groundLayer = 6;
    float _groundLength = 0.53f;
    public bool Ground => _onGround;
    bool _onGround;

    void Update()
    {
        CheckGround();
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
}
