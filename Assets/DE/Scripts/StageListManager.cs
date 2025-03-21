using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageListManager : MonoBehaviour
{
    private GameManager _gameManager;
    private EventSystem _eventSystem;
    private Transform _content;
    private List<GameObject> _stageBtnList = new List<GameObject>();
    [SerializeField] private StageButton stageBtnPrefab;
    

    private void Start()
    {
        _gameManager = GameManager.Instance;
        _content = FindAnyObjectByType<HorizontalLayoutGroup>().transform;
        _eventSystem = FindAnyObjectByType<EventSystem>();

        foreach (var stage in _gameManager.StageDataList)
        {
            var newBtn = Instantiate(stageBtnPrefab, _content);
            _stageBtnList.Add(newBtn.gameObject);
            newBtn.Init(stage);
        }
        
        _eventSystem.firstSelectedGameObject = _stageBtnList[0].gameObject;
    }
}
