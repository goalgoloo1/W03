using UnityEngine;
using Enums;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "StageData", menuName = "Scriptable Objects/StageData")]
public class StageData : ScriptableObject
{
    public int StageIndex;
    public int FinalCoinCount;
    public RankType FinalRankType;
    public float FinalTotalTime;
}
