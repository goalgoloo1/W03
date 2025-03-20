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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player") && (_coMove == null))
        {
            _coMove = StartCoroutine(CoMove(_startPoint.position, _endPoint.position, _moveTime));
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
            _startPoint.position = end;
            _endPoint.position = start;

            _coMove = null;
        }
    }
}
