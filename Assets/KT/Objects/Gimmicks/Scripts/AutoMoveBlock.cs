using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class AutoMoveBlock : MonoBehaviour
{
    Transform _startPoint;
    Transform _endPoint;
    [SerializeField] float _moveTime = 1f;
    Coroutine _coMove;

    void Start()
    {
        _startPoint = transform.parent.GetComponentInChildren<StartPoint>().transform;
        _endPoint = transform.parent.GetComponentInChildren<EndPoint>().transform;

        _coMove = StartCoroutine(CoMove(_startPoint.localPosition, _endPoint.localPosition, _moveTime));
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
            //��ġ �ٲٱ� -> �ٽ� ȣ�� �Ǿ��� �� �ڷ� ���ư� 
            _startPoint.localPosition = end;
            _endPoint.localPosition = start;

            StopCoroutine(_coMove);
            _coMove = StartCoroutine(CoMove(_startPoint.localPosition, _endPoint.localPosition, _moveTime));
        }
    }
}
