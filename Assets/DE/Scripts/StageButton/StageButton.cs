using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    private GameManager _gameManager;

    private int _stageIndex;
    private TMP_Text _stageText;
    private TMP_Text _rankText;
    private StageBtnCoinGroup _coinGroup;
    private Button _button;

    private void Awake()
    {
        _gameManager = GameManager.Instance;

        _button = GetComponent<Button>();
        _stageText = GetComponentInChildren<StageBtnStageText>().GetComponent<TMP_Text>();
        _rankText = GetComponentInChildren<StageBtnRankText>().GetComponent<TMP_Text>();
        _coinGroup = GetComponentInChildren<StageBtnCoinGroup>();
    }

    public void Init(StageData stageData)
    {
        _stageIndex = stageData.StageIndex;
        
        _stageText.text = $"Stage {stageData.StageIndex + 1}";
        _rankText.text = $"{stageData.FinalRankType.ToString()}";
        _coinGroup.ChangeCoinAlpha(stageData.FinalCoinCount);
        
        _button.onClick.AddListener(OnClickStageBtn);
    }

    private void OnClickStageBtn()
    {
        _gameManager.OnSelectStageEvent?.Invoke(_stageIndex);
    }
}
