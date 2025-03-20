using UnityEngine;
using Enums;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData")]
public class StageData : ScriptableObject
{
    public int stageIndex;
    public float finalTotalScore;
    public RankType finlaRankType;
    public float finalTotalTime;
}
