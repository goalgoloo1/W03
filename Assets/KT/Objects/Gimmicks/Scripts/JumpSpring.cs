using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class JumpSpring : MonoBehaviour
{
    [SerializeField] float _jumpPower = 50f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.transform.CompareTag("Player"))
        {
            Rigidbody2D rigid = collision.transform.GetComponent<Rigidbody2D>();
            if (rigid != null)
            {
                Vector2 dir = (collision.transform.position - transform.position).normalized;
                
                Vector2 jumpDir = transform.up * _jumpPower;
                //rigid.AddForce(jumpDir, ForceMode2D.Impulse);
                rigid.linearVelocity += jumpDir;
            }
        }
    }
}
