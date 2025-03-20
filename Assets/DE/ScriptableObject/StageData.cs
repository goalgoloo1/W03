using UnityEngine;
using Enums;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData")]
public class StageData : ScriptableObject
{
    public int StageIndex;
    public float FinalTotalScore;
    public RankType FinalRankType;
    public float FinalTotalTime;
}
