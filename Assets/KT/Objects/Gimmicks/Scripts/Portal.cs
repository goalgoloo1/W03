using UnityEngine;

public class Portal : MonoBehaviour
{
    Transform _exit; //출구

    private void Start()
    {
        _exit = transform.parent.GetComponentInChildren<PortalExit>().GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Teleport(collider);
        }
    }

    void Teleport(Collider2D collider)
    {
        //위치 이동
        collider.transform.position = _exit.position;

        //힘 방향 조정 (벡터 회전)
        float portalDeg = _exit.eulerAngles.z - transform.eulerAngles.z;
        float rad = portalDeg * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);

        Rigidbody2D collRigid = collider.GetComponent<Rigidbody2D>();
        Vector2 collVelocity = collRigid.linearVelocity;

        float x = collVelocity.x * cos - collVelocity.y * sin;
        float y = collVelocity.x * sin + collVelocity.y * cos;

        collRigid.linearVelocity = new Vector2(x, y);
    }
}
