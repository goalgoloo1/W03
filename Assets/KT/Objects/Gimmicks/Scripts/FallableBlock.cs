using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class FallableBlock : MonoBehaviour
{
    Rigidbody2D _rigid;

    Coroutine _coFall;
    WaitForSeconds _wait = new WaitForSeconds(1.0f);

    private void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && (_coFall == null))
        {
            _coFall = StartCoroutine(CoFall());
        }
    }

    IEnumerator CoFall()
    {
        yield return _wait;
        _rigid.constraints = RigidbodyConstraints2D.None;
        _rigid.constraints = RigidbodyConstraints2D.FreezePositionX;
        _rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
