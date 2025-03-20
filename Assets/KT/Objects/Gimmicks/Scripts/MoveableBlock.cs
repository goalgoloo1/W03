using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class MovableBlock : MonoBehaviour
{
    Transform _startPoint;
    Transform _endPoint;
    float _moveTime = 1f;
    Coroutine _coMove;

    void Start()
    {
        _startPoint = transform.parent.GetComponentInChildren<StartPoint>().transform;
        _endPoint = transform.parent.GetComponentInChildren<EndPoint>().transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player") && (_coMove == null))
        {
            _coMove = StartCoroutine(CoMove(collision.transform, _startPoint.position, _endPoint.position, _moveTime));
        }
    }

    IEnumerator CoMove(Transform target, Vector3 start, Vector3 end, float moveTime)
    {
        target.SetParent(transform);
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
            //��ġ �ٲٱ� -> �ٽ� ȣ�� �Ǿ��� �� �ڷ� ���ư� 
            _startPoint.position = end;
            _endPoint.position = start;

            target.SetParent(null);
            _coMove = null;
        }
    }
}
