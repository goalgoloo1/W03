using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class FallableBlock : MonoBehaviour
{
    Rigidbody2D _rigid;

    Coroutine _coFall;
    [SerializeField] float _fallDelay;

    private void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.CompareTag("Player") && (_coFall == null))
        {
            _coFall = StartCoroutine(CoFall());
        }
    }

    IEnumerator CoFall()
    {
        yield return new WaitForSeconds(_fallDelay);
        _rigid.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
    }
}
