using UnityEngine;
using Enums;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            RankType rank = GameManager.Instance.StageManager.GetCurrentFinalRankRankType();
            float timer = GameManager.Instance.StageManager.GetTotalTimeFloat();
            //int coinCount = GameManager.Instance.StageManager.GetCurrentFinalScoreFloat;

            //GameManager.StageManager.OnStageClearEvent?.Invoke(rank, timer, coinCount);
        }
    }
}
