using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class JumpSpring : MonoBehaviour
{
    [SerializeField] float _jumpPower = 50f;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.CompareTag("Player"))
        {
            Rigidbody2D rigid = collider.transform.GetComponent<Rigidbody2D>();
            if (rigid != null)
            {
                Vector2 dir = (collider.transform.position - transform.position).normalized;
                
                Vector2 jumpDir = transform.up * _jumpPower;

                //���� ������ �����
                rigid.linearVelocity = jumpDir;
            }
        }
    }
}
