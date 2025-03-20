using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class BreakableBlock : MonoBehaviour
{
    GameObject _body;
    PolygonCollider2D _polygonColl;
    
    Coroutine _coBreak;
    WaitForSeconds _wait = new WaitForSeconds(1.0f);

    private void Start()
    {
        _body = GetComponentInChildren<SpriteRenderer>().gameObject;
        _polygonColl = GetComponent<PolygonCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.transform.CompareTag("Player") && (_coBreak == null))
        if (_coBreak == null)
        {
            _coBreak = StartCoroutine(CoBreak());
        }
    }

    IEnumerator CoBreak()
    {
        yield return _wait;

        _polygonColl.enabled = false;
        _body.SetActive(false);
        //Debug.Log("break");

        yield return _wait;
        _polygonColl.enabled = true;
        _body.SetActive(true);

        _coBreak = null;
    }
}
