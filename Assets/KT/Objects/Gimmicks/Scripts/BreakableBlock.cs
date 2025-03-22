using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class BreakableBlock : MonoBehaviour
{
    GameObject _body;
    PolygonCollider2D _polygonColl;
    
    Coroutine _coBreak;
    [SerializeField] float _breakDelay;
    [SerializeField] float _respawnDelay;

    private void Start()
    {
        _body = GetComponentInChildren<SpriteRenderer>().gameObject;
        _polygonColl = GetComponent<PolygonCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //if (collider.transform.CompareTag("Player") && (_coBreak == null))
        if (_coBreak == null)
        {
            _coBreak = StartCoroutine(CoBreak());
        }
    }

    IEnumerator CoBreak()
    {
        yield return new WaitForSeconds(_breakDelay);

        _polygonColl.enabled = false;
        _body.SetActive(false);
        //Debug.Log("break");

        yield return new WaitForSeconds(_respawnDelay);
        _polygonColl.enabled = true;
        _body.SetActive(true);

        _coBreak = null;
    }
}
