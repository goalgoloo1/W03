using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    Rigidbody2D _rigid;
    float velocityY;

    bool _canJump = true;

    float _jumpHeight = 5f;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();

        InputManager.Instance.OnJumpEvent += Jump;
    }
    void FixedUpdate()
    {
        CheckGround();
    }
    
    void CheckGround()
    {
        Vector2 origin = transform.position - new Vector3(0, transform.localScale.y * 0.5f, 0);
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, transform.localScale * 0.5f, 0, Vector2.down, 0.26f, 1 << 6);

        //if (hit.transform.name == "Ground")
        //{
            _canJump = true;
        //}
    }

    void Jump()
    {
        if (_canJump)
        {
            velocityY = _jumpHeight;
            
            _rigid.linearVelocityY = velocityY;
            Debug.Log(_rigid.linearVelocity);
            _canJump = false;
        }
    }
}
