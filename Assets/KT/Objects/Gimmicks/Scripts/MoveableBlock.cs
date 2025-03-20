using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class MovableBlock : MonoBehaviour
{
    Vector3 _startPos;
    Vector3 _endPos;
    float _moveTime = 1f;
    Coroutine _coMove;

    void Start()
    {
        _startPos = transform.parent.GetComponentInChildren<StartPoint>().transform.position;
        _endPos = transform.parent.GetComponentInChildren<EndPoint>().transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && (_coMove == null))
        {
            _coMove = StartCoroutine(CoMove(_startPos, _endPos, _moveTime));
        }
    }

    IEnumerator CoMove(Vector3 start, Vector3 end, float moveTime)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / moveTime;

            transform.localPosition = Vector3.Lerp(start, end, percent);

            yield return null;
        }

        if (percent >= 1)
        {
            //위치 바꾸기 -> 다시 호출 되었을 때 뒤로 돌아감 
            _startPos = end;
            _endPos = start;
            _coMove = null;
        }
    }
}
