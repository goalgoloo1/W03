using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageListManager : MonoBehaviour
{
    private GameManager _gameManager;
    private EventSystem _eventSystem;
    
    private ScrollRect _scrollRect;
    private RectTransform _content;
    private RectTransform _viewport;
    private GameObject _previousSelected;
    
    private List<GameObject> _stageBtnList = new List<GameObject>();
    [SerializeField] private StageButton stageBtnPrefab;

    private void Awake()
    {
        _gameManager = GameManager.Instance;
        _eventSystem = FindAnyObjectByType<EventSystem>();

        _scrollRect = FindAnyObjectByType<ScrollRect>();
        _content = _scrollRect.content;
        _viewport = _scrollRect.viewport;
    }
    
    private void Start()
    {
        foreach (var stage in _gameManager.StageDataList)
        {
            var newBtn = Instantiate(stageBtnPrefab, _content.transform);
            _stageBtnList.Add(newBtn.gameObject);
            newBtn.Init(stage);
        }
        
        _eventSystem.firstSelectedGameObject = _stageBtnList[0].gameObject;
    }

    private void Update()
    {
        var currentSelected = _eventSystem.currentSelectedGameObject;

        if (currentSelected == _previousSelected)
        {
            return;
        }

        if (_eventSystem.currentSelectedGameObject == null)
        {
            return;
        }
        
        var selected = _eventSystem.currentSelectedGameObject.GetComponent<RectTransform>();
        UpdateScrollbarValue(selected);
        
        _previousSelected = currentSelected;
    }

    private void UpdateScrollbarValue(RectTransform target)
    {
        var targetPos = _content.InverseTransformPoint(target.position);
        var viewportPos = _content.InverseTransformPoint(_viewport.position);
        var offset = targetPos.x - viewportPos.x;
        
        var normalizedOffset = offset / (_content.rect.width - _viewport.rect.width);
        var newNormalizedPos =_scrollRect.horizontalNormalizedPosition + normalizedOffset;
        
        _scrollRect.horizontalNormalizedPosition = newNormalizedPos;
    }
}
