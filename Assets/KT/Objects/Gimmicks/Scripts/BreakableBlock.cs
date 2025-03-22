using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class BreakableBlock : MonoBehaviour
{
    [SerializeField] Sprite _normal;
    [SerializeField] Sprite _crack;
    SpriteRenderer _renderer;
    GameObject _body;
    PolygonCollider2D _polygonColl;
    
    Coroutine _coBreak;
    [SerializeField] float _breakDelay;
    [SerializeField] float _respawnDelay;

    private void Start()
    {
        _renderer = GetComponentInChildren<SpriteRenderer>();
        _body = _renderer.gameObject;
        _polygonColl = GetComponent<PolygonCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.CompareTag("Player") && (_coBreak == null))
        {
            _coBreak = StartCoroutine(CoBreak());
        }
    }

    IEnumerator CoBreak()
    {
        _renderer.sprite = _crack;
        yield return new WaitForSeconds(_breakDelay);

        _polygonColl.enabled = false;
        _body.SetActive(false);
        //Debug.Log("break");

        yield return new WaitForSeconds(_respawnDelay);
        _polygonColl.enabled = true;
        _body.SetActive(true);
        _renderer.sprite = _normal;

        _coBreak = null;
    }
}
