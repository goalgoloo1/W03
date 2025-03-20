using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class BreakableBlock : MonoBehaviour
{
    Coroutine _coBreak;
    WaitForSeconds wait = new WaitForSeconds(1.0f);

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && (_coBreak == null))
        {
            _coBreak = StartCoroutine(CoBreak());
        }
    }

    IEnumerator CoBreak()
    {
        yield return wait;

        gameObject.SetActive(false);
        //Debug.Log("break");

        _coBreak = null;
    }
}
